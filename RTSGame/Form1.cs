using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTSGame
{

    public partial class RTSGame : Form
    {
        #region Variables
        int seconds = 0;
        int minutes = 0;
        private bool loaded;
        GameEngine gameEngine = new GameEngine();
        #endregion

        #region Methods
        public RTSGame()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (loaded == true)
            {
                tmrGameTimer.Start();
            }
            else
            {
                gameEngine.setGame();
                tmrGameTimer.Start();
            }
        }

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            lblTimer.Text = TimeSpan.FromSeconds(seconds).ToString("mm\\:ss");
            lblResourcesAvailable.Text = Convert.ToString(((ResourceBuilding)gameEngine.Map.BuildingsOnMap[1]).AvailableResources);
            gameEngine.Combat();
            rtbMap.Text = "";
            for (int i = 0; i < gameEngine.Map.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < gameEngine.Map.Grid.GetLength(1); j++)
                {
                    rtbMap.Text += gameEngine.Map.Grid[i, j];
                }
                rtbMap.Text += Environment.NewLine;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            tmrGameTimer.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult result = MessageBox.Show("Are you sure?",
            "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void rtbMap_MouseClick(object sender, MouseEventArgs e)
        {
            lblUnitInformation.Text = "";
            int mouseX = MousePosition.X;
            int mouseY = MousePosition.Y;

            int formX = this.Location.X;
            int formY = this.Location.Y;

            int y = (mouseX - formX - 44) / 18;
            int x = (mouseY - formY - 74) / 17;

            x = x + 1;

            foreach (Unit u in gameEngine.Map.UnitsonMap)
            {
                if (u.X == x && u.Y == y)
                {
                    lblUnitInformation.Text += u.toString();
                }
            }
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            gameEngine.saveGame();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            loaded = true;
            gameEngine.loadGame();
            rtbMap.Text = "";
            for (int i = 0; i < gameEngine.Map.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < gameEngine.Map.Grid.GetLength(1); j++)
                {
                    rtbMap.Text += gameEngine.Map.Grid[i, j];
                }
                rtbMap.Text += Environment.NewLine;
            }

            lblUnitInformation.Text = Convert.ToString(gameEngine.Map.UnitsOnMapNum);

        }

        private void RTSGame_Load(object sender, EventArgs e)
        {
            gameEngine.Initialize();
            for (int i = 0; i < gameEngine.Map.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < gameEngine.Map.Grid.GetLength(1); j++)
                {
                    rtbMap.Text += gameEngine.Map.Grid[i, j];
                }
                rtbMap.Text += Environment.NewLine;
            }
        }

        private void btnUseResources_Click(object sender, EventArgs e)
        {
            int spendAmount;
            spendAmount = int.Parse(txtResourcesUse.Text);
            gameEngine.spendResources(spendAmount);
        }
        #endregion
    }
}
