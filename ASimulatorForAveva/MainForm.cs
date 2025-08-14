using ASimulatorForAveva.Modbus;
using ASimulatorForAveva.Models.Simulation;
using ASimulatorForAveva.Objects;
using ASimulatorForAveva.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASimulatorForAveva
{
    public partial class MainForm : Form
    {
        private bool isModbusRunning = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.mixers = new List<Mixer>
            {
                new Mixer(1, 15, 2048)
                //new Mixer(2, 10, 1024),
                //new Mixer(3, 5, 3072),
                //new Mixer(4, 20, 2457),
                //new Mixer(5, 10, 3072),
                //new Mixer(6, 5, 2457),
                //new Mixer(7, 20, 1024),
                //new Mixer(8, 15, 2048)
            };

            checkBoxRunSimulation.Enabled = true;
        }

        private void mixerSimulationTimer_Tick(object sender, EventArgs e)
        {
            HumanReadableRichTextBox.Clear();

            foreach (var mixer in Globals.mixers)
            {
                HardcodedProcess.SimulateStep(mixer);
                HumanReadableRichTextBox.AppendText(GetMixerStatsAsText.get(mixer));
            }
        }

        private async void startStopButton_Click(object sender, EventArgs e)
        {
            if (!isModbusRunning)
            {
                // Start the Modbus server asynchronously.
                // Await allows the UI to stay responsive.

                await ModbusProcess.StartAsync();
                isModbusRunning = true;

                startStopButton.Text = "Stop Modbus Server";
            }
            else
            {
                startStopButton.Text = "Stopping...";

                await ModbusProcess.StopAsync();
                isModbusRunning = false;

                startStopButton.Text = "Start Modbus Server";
                startStopButton.Enabled = true;
            }
        }

        private void checkBoxRunSimulation_CheckedChanged(object sender, EventArgs e)
        {
            mixerSimulationTimer.Enabled = checkBoxRunSimulation.Checked;
        }

        private void buttonRestartMBTCP_Click(object sender, EventArgs e)
        {
            RestartMBTCPDriver.restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessDescription processDescription = new ProcessDescription();

            processDescription.Show();
        }

        private void buttonRedeployGalaxy_Click(object sender, EventArgs e)
        {
            //TODO: Implement galaxy redeployment logic using
            //C:\Program Files (x86)\Common Files\ArchestrA\ArchestrA.GRAccess.dll
        }
    }
}