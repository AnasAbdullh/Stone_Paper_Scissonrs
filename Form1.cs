using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stone_Paper_Scissonrs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private enum StautsWinner
        {
            Player1,
            Computer,
            Darw,
        }

        private enum ChoicePlayers
        {
            Stone = 1,
            Paper = 2,
            Scissors = 3,
        }

        private struct ResultGame
        {
            public int GameRounds;
            public int Player1WonTimes;
            public int ComputerWonTimes;
            public int DrawTimes;
            public StautsWinner winner;
        }

        private ResultGame result;
        private ChoicePlayers player1;
        private ChoicePlayers computer;

        private int CounTimes;

        private void SetTextBeginAndRound()
        {
            label2.Text = $"Round [ {CounTimes} ] begins :";
            label4.Text = $"---------------------Round [ {CounTimes} ]-------------------------\r\n\r\n";
        }

        private Random rand = new Random();

        private ChoicePlayers ChoiceComputer()
        {
            computer = (ChoicePlayers)rand.Next(1, 4);
            return computer;
        }
        private ChoicePlayers ChoicePlayer1() => player1;



        private StautsWinner ChoiceWinner()
        {
            player1 = ChoicePlayer1();
            computer = ChoiceComputer();

            if (player1 == computer)
            {
                return StautsWinner.Darw;
            }

            switch (player1)
            {
                case ChoicePlayers.Stone:
                    if (computer == ChoicePlayers.Paper)
                        return StautsWinner.Computer;
                    break;
                case ChoicePlayers.Scissors:
                    if (computer == ChoicePlayers.Stone)
                        return StautsWinner.Computer;
                    break;
                case ChoicePlayers.Paper:
                    if (computer == ChoicePlayers.Scissors)
                        return StautsWinner.Computer;
                    break;
                default:
                    break;
            }
            return StautsWinner.Player1;
        }

        private void SetLableAndBackColor(string txt, Color color)
        {
            label11.Text = txt;
            panel2.BackColor = color;
        }

        private void SetWinnerInRound(StautsWinner winner)
        {
            switch (winner)
            {
                case StautsWinner.Player1:
                    SetLableAndBackColor("Player1", Color.Green);
                    result.Player1WonTimes++;
                    break;
                case StautsWinner.Computer:
                    SetLableAndBackColor("Computer", Color.Red);
                    result.ComputerWonTimes++;
                    break;
                case StautsWinner.Darw:
                    SetLableAndBackColor("Draw", Color.Yellow);
                    result.DrawTimes++;
                    break;
                default:
                    break;


            }

            label10.Text = player1.ToString();
            label9.Text = computer.ToString();

        }

        private void ChoiceFinallyWinner()
        {
            int CountWinnerPlayer = result.Player1WonTimes;
            int CountWinnerComputer = result.ComputerWonTimes;

            if (CountWinnerPlayer > CountWinnerComputer)
            {
                result.winner = StautsWinner.Player1;
                return;
            }

            if (CountWinnerComputer == 0 && CountWinnerPlayer == 0)
            {
                result.winner = StautsWinner.Darw;
                return;
            }

            result.winner = StautsWinner.Computer;
        }

        private void FillOutResultGame()
        {
            panel3.Visible = true;

            label17.Text = result.GameRounds.ToString();
            label18.Text = result.Player1WonTimes.ToString();
            label19.Text = result.ComputerWonTimes.ToString();
            label24.Text = result.DrawTimes.ToString();
            label21.Text = result.winner.ToString();

        }

        private void AnswerPlayAgain()
        {
            if (textBox3.Text.ToLower() == "y")
            {
                result = default;
                player1 = default;
                computer = default;
                CounTimes = default;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                textBox1.Clear();
                textBox2.Clear(); 
                textBox3.Clear();
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                return;
            }
            Application.Exit();
        }

        private void StartGame()
        {
            if (CounTimes < result.GameRounds)
            {
                CounTimes++;
                SetTextBeginAndRound();
                SetWinnerInRound(ChoiceWinner());
            }

            if (CounTimes >= result.GameRounds)
            {
                textBox2.ReadOnly = true;
                ChoiceFinallyWinner();
                FillOutResultGame();
                return;
            }
            textBox2.Clear();
        }

        private bool ValidationInput(TextBox textbox, bool isChoicePlayer = false)
        {
            if (string.IsNullOrWhiteSpace(textbox.Text.Trim()))
            {
                MessageBox.Show("من فضلك ادخل رقم");
                return false;
            }


            if (!int.TryParse(textbox.Text, out int value))
            {
                MessageBox.Show("الرجاء إدخال رقم صحيح");
                return false;
            }


            if (value <= 0)
            {
                MessageBox.Show("الرجاء إدخال رقم أكبر من صفر");
                return false;
            }

            if (isChoicePlayer && value > 3)
            {
                MessageBox.Show("ادخل رقم بين الواحد والثالثة");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidationInput(textBox1))
            {
                panel1.Visible = true;
                result.GameRounds = int.Parse(textBox1.Text);
                textBox1.ReadOnly = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidationInput(textBox2, true))
            {
                panel2.Visible = true;
                player1 = (ChoicePlayers)int.Parse(textBox2.Text);
                StartGame();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnswerPlayAgain();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}