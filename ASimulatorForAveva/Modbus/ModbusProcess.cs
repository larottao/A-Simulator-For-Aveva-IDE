using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; // Assuming a WinForms context for MessageBox

namespace ASimulatorForAveva.Modbus
{
    public static class ModbusProcess
    {
        private const int port = 502;
        private const int maxRegisters = 1000;
        private static ushort[] inputRegisters = new ushort[maxRegisters];
        private static TcpListener listener;
        private static CancellationTokenSource cancellationTokenSource;
        private static readonly object registerLock = new object(); // Lock for thread-safe access to inputRegisters
        private static Task formatDataTask;
        private static Task listenTask;

        public static async Task StartAsync()
        {
            if (cancellationTokenSource != null)
            {
                MessageBox.Show("Modbus process is already running.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            try
            {
                // Start the data formatting and client listening tasks
                formatDataTask = Task.Run(() => FormatDataToHardcodedExample(token), token);
                listenTask = ListenForClientsAsync(token);

                await Task.WhenAll(formatDataTask, listenTask);
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Modbus process startup was canceled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during Modbus process startup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ensure everything is cleaned up if an error occurs during startup
                await StopAsync();
            }
        }

        private static async Task ListenForClientsAsync(CancellationToken token)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Debug.WriteLine($"Modbus TCP server running on port {port}");

                while (!token.IsCancellationRequested)
                {
                    TcpClient client;
                    try
                    {
                        // Wait for a client connection, but check the token regularly
                        var acceptTask = listener.AcceptTcpClientAsync();
                        client = await acceptTask.WithCancellation(token);
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected exception when the listener is stopped
                        return;
                    }

                    // Handle each client in a separate task
                    _ = Task.Run(() => HandleClientAsync(client, token), token);
                }
            }
            catch (SocketException ex)
            {
                if (token.IsCancellationRequested)
                {
                    Debug.WriteLine("Modbus TCP server stopped.");
                }
                else
                {
                    MessageBox.Show($"A socket error occurred: {ex.Message}", "Socket Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine($"Socket exception: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in the listener: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Listener exception: {ex.Message}");
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                    Debug.WriteLine("TCP listener stopped.");
                }
            }
        }

        public static async Task StopAsync()
        {
            if (cancellationTokenSource == null) return;

            cancellationTokenSource.Cancel();

            // Wait for both tasks to gracefully exit.
            try
            {
                await Task.WhenAll(formatDataTask, listenTask);
            }
            catch (OperationCanceledException)
            {
                // This is expected. We just need to ensure the tasks finish their cleanup.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while stopping the Modbus process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                Debug.WriteLine("Modbus process has been stopped and resources cleaned up.");
            }
        }

        private static void FormatDataToHardcodedExample(CancellationToken token)
        {
            Random rnd = new Random();
            while (!token.IsCancellationRequested)
            {
                lock (registerLock) // Ensure thread-safe access to inputRegisters
                {
                    // Accessing Globals.mixers needs to be thread-safe.
                    // Assuming it's a simple List and operations are atomic enough for this simulation.
                    // For a real-world application, you would need a lock here.
                    if (Globals.mixers.Count > 0)
                    {
                        inputRegisters[0] = (ushort)Globals.mixers[0].level.Value; // Level.PV
                        inputRegisters[1] = (ushort)Globals.mixers[0].temperatureSensor.Value; // Temperature.PV
                        inputRegisters[2] = (ushort)Globals.mixers[0].inletValve1.Position; // Inlet1.Position
                        inputRegisters[3] = (ushort)Globals.mixers[0].inletValve2.Position; // Inlet2.Position
                        inputRegisters[4] = (ushort)Globals.mixers[0].outletValve.Position; // Outlet.Position
                        inputRegisters[5] = (ushort)Globals.mixers[0].ingredient1Pump.SpeedPV; // Pump1.Speed.PV
                        inputRegisters[7] = (ushort)Globals.mixers[0].ingredient2Pump.SpeedPV; // Pump2.Speed.PV
                        inputRegisters[9] = (ushort)Globals.mixers[0].agitator.SpeedPV; // Agitator.Speed.PV
                        inputRegisters[13] = (ushort)Globals.mixers[0].currentProcessStep; // ProcessStep
                        inputRegisters[14] = (ushort)Globals.mixers[0].ing1CurrentLiters.Value; // Totalizer1.PV
                        inputRegisters[15] = (ushort)Globals.mixers[0].ing2CurrentLiters.Value; // Totalizer2.PV
                        inputRegisters[16] = (ushort)Globals.mixers[0].combinedCurrentLiters.Value; // Totalizer3.PV
                        inputRegisters[17] = (ushort)rnd.Next(3400, 4000); // TT1.PV
                        inputRegisters[18] = (ushort)rnd.Next(3300, 3900); // TT2.PV
                        inputRegisters[19] = (ushort)rnd.Next(1500, 2100); // TT3.PV
                        inputRegisters[20] = (ushort)rnd.Next(1000, 1600); // TT4.PV
                        inputRegisters[21] = (ushort)(DateTime.Now.Second); // MixingTime.PV
                    }
                }

                try
                {
                    Task.Delay(500, token).Wait();
                }
                catch (OperationCanceledException)
                {
                    // This is expected when the token is canceled.
                    return;
                }
            }
        }

        private static async Task HandleClientAsync(TcpClient client, CancellationToken token)
        {
            NetworkStream stream = null;
            byte[] buffer = new byte[256];

            try
            {
                stream = client.GetStream();
                Debug.WriteLine("Client connected.");

                while (!token.IsCancellationRequested)
                {
                    Debug.WriteLine($"{DateTime.Now} Pooling received from AVEVA!");

                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0) break; // Client disconnected

                    if (bytesRead < 12)
                    {
                        Debug.WriteLine("Received invalid Modbus TCP frame (too short).");
                        continue;
                    }

                    ushort transactionId = (ushort)((buffer[0] << 8) | buffer[1]);
                    byte unitId = buffer[6];
                    byte functionCode = buffer[7];

                    if (functionCode == 4) // Read Input Registers
                    {
                        ushort startAddress = (ushort)((buffer[8] << 8) | buffer[9]);
                        ushort quantity = (ushort)((buffer[10] << 8) | buffer[11]);

                        if (startAddress + quantity <= maxRegisters)
                        {
                            byte[] response;
                            lock (registerLock) // Lock to read from the shared array
                            {
                                response = CreateReadResponse(transactionId, unitId, functionCode, startAddress, quantity);
                            }
                            await stream.WriteAsync(response, 0, response.Length, token);
                        }
                        else
                        {
                            await SendExceptionAsync(stream, transactionId, unitId, functionCode, 0x02, token); // Illegal data address
                        }
                    }
                    else
                    {
                        await SendExceptionAsync(stream, transactionId, unitId, functionCode, 0x01, token); // Illegal function
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Client handling task was canceled.");
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"An I/O error occurred with the client: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while handling a client: {ex.Message}", "Client Handling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"HandleClient exception: {ex.Message}");
            }
            finally
            {
                stream?.Close();
                client?.Close();
                Debug.WriteLine("Client disconnected.");
            }
        }

        private static byte[] CreateReadResponse(ushort transactionId, byte unitId, byte functionCode, ushort startAddress, ushort quantity)
        {
            byte[] response = new byte[9 + quantity * 2];
            response[0] = (byte)(transactionId >> 8);
            response[1] = (byte)(transactionId & 0xFF);
            response[2] = 0; // Protocol ID high
            response[3] = 0; // Protocol ID low
            response[4] = (byte)((3 + quantity * 2) >> 8); // Length high
            response[5] = (byte)((3 + quantity * 2) & 0xFF); // Length low
            response[6] = unitId;
            response[7] = functionCode;
            response[8] = (byte)(quantity * 2);

            for (int i = 0; i < quantity; i++)
            {
                ushort value = inputRegisters[startAddress + i];
                response[9 + i * 2] = (byte)(value >> 8);
                response[10 + i * 2] = (byte)(value & 0xFF);
            }
            return response;
        }

        private static async Task SendExceptionAsync(NetworkStream stream, ushort transactionId, byte unitId, byte functionCode, byte exceptionCode, CancellationToken token)
        {
            byte[] response = new byte[9];
            response[0] = (byte)(transactionId >> 8);
            response[1] = (byte)(transactionId & 0xFF);
            response[2] = 0;
            response[3] = 0;
            response[4] = 0;
            response[5] = 3;
            response[6] = unitId;
            response[7] = (byte)(functionCode + 0x80);
            response[8] = exceptionCode;
            await stream.WriteAsync(response, 0, response.Length, token);
        }
    }

    // Helper extension to make AcceptTcpClientAsync cancellable
    public static class TaskExtensions
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }
            return await task;
        }
    }
}