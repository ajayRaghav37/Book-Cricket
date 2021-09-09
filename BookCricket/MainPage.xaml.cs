using BookCricket.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BookCricket
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string player1;
        string player2;
        int player1Runs = 0, player2Runs = 0, player1Wkts = 0, player2Wkts = 0, player1Balls = 0, player2Balls = 0;
        int tossResult;
        PathGeometry geop = new PathGeometry();
        PathFigure figp = new PathFigure();
        PathGeometry geop2 = new PathGeometry();
        PathFigure figp2 = new PathFigure();
        PathGeometry geop3 = new PathGeometry();
        PathFigure figp3 = new PathFigure();
        PathGeometry geop4 = new PathGeometry();
        PathFigure figp4 = new PathFigure();

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void btnToss_Click(object sender, RoutedEventArgs e)
        {
            txtPlayer1.Text = txtPlayer1.Text.Trim();
            txtPlayer2.Text = txtPlayer2.Text.Trim();
            if (txtPlayer1.Text == txtPlayer2.Text)
            {
                lblToss.Text = "Player names are same";
                txtPlayer1.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
            else
            {
                if (txtPlayer1.Text == player2 && txtPlayer2.Text == player1)
                {
                    string temp;
                    temp = (string)lblWins1.Content;
                    lblWins1.Content = lblWins2.Content;
                    lblWins2.Content = temp;
                }
                else if (txtPlayer1.Text != player1 || txtPlayer2.Text != player2)
                {
                    lblWins1.Content = "0";
                    lblWins2.Content = "0";
                }
                figp.StartPoint = new Point(0, 300);
                figp2.StartPoint = new Point(0, 300);
                figp.Segments.Clear();
                figp2.Segments.Clear();
                geop.Figures.Clear();
                geop.Figures.Add(figp);
                geop2.Figures.Clear();
                geop2.Figures.Add(figp2);
                figp3.StartPoint = new Point(0, 300);
                figp4.StartPoint = new Point(0, 300);
                figp3.Segments.Clear();
                figp4.Segments.Clear();
                geop3.Figures.Clear();
                geop3.Figures.Add(figp3);
                geop4.Figures.Clear();
                geop4.Figures.Add(figp4);
                btnFlip.Content = "FLIP BOOK";
                lblOver1.Text = "Over 1";
                lblOver2.Text = "Over 2";
                lblOver3.Text = "Over 3";
                lblOver4.Text = "Over 4";
                lblOver5.Text = "Over 5";
                player1Runs = 0;
                player1Balls = 0;
                player1Wkts = 0;
                player2Runs = 0;
                player2Balls = 0;
                player2Wkts = 0;
                lblScore1.Text = "";
                lblScore2.Text = "";
                lblPlayer1.Text = "";
                lblPlayer2.Text = "";
                player1 = txtPlayer1.Text;
                player2 = txtPlayer2.Text;
                Random rnd = new Random();
                tossResult = rnd.Next(2);
                if (tossResult == 0)
                    lblToss.Text = player1 + " won the toss";
                else
                    lblToss.Text = player2 + " won the toss";
                txtPlayer1.IsEnabled = false;
                txtPlayer2.IsEnabled = false;
                btnToss.IsEnabled = false;
                btnBat.IsEnabled = true;
                btnBowl.IsEnabled = true;
                btnBat.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
        }

        private void btnBat_Click(object sender, RoutedEventArgs e)
        {
            if (tossResult == 0)
            {
                lblPlayer1.Text = player1;
                lblPlayer2.Text = player2;
            }
            else
            {
                lblPlayer1.Text = player2;
                lblPlayer2.Text = player1;
            }
            btnBat.IsEnabled = false;
            btnBowl.IsEnabled = false;
            btnFlip.IsEnabled = true;
            lblScore1.Text = "0/0";
            lblScore2.Text = "Yet to bat";
            btnFlip.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        private void btnBowl_Click(object sender, RoutedEventArgs e)
        {
            if (tossResult == 0)
            {
                lblPlayer1.Text = player2;
                lblPlayer2.Text = player1;
            }
            else
            {
                lblPlayer1.Text = player1;
                lblPlayer2.Text = player2;
            }
            btnBat.IsEnabled = false;
            btnBowl.IsEnabled = false;
            btnFlip.IsEnabled = true;
            lblScore1.Text = "0/0";
            lblScore2.Text = "Yet to bat";
            btnFlip.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            if (lblScore2.Text == "0/0")
            {
                lblOver1.Text = "Over 1";
                lblOver2.Text = "Over 2";
                lblOver3.Text = "Over 3";
                lblOver4.Text = "Over 4";
                lblOver5.Text = "Over 5";
            }
            bookRight.Visibility = Windows.UI.Xaml.Visibility.Visible;
            bookLeft.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255,240,240,240));
            Random rnd = new Random();
            int result = rnd.Next(1, 50) * 2;
            pgLeft.Text = Convert.ToString(result);
            pgRight.Text = Convert.ToString((result + 1));
            string ballResult;
            ballResult = " " + Convert.ToString(result % 10);
            if (lblScore2.Text == "Yet to bat")
                player1Runs += (result % 10);
            else
                player2Runs += (result % 10);
            if (result % 10 == 8)
            {
                if (lblScore2.Text == "Yet to bat")
                    player1Runs -= 7;
                else
                    player2Runs -= 7;
                ballResult = " 1";
            }
            if (result % 10 == 0)
            {
                if (lblScore2.Text == "Yet to bat")
                    player1Wkts++;
                else
                    player2Wkts++;
                ballResult = " W";
            }
            if (lblScore2.Text == "Yet to bat")
                player1Balls++;
            else
                player2Balls++;
            if (lblScore2.Text == "Yet to bat")
            {
                if (player1Balls % 6 == 1)
                    ballResult = "     " + ballResult;
            }
            else
            {
                if (player2Balls % 6 == 1)
                    ballResult = "     " + ballResult;
            }
            if (lblScore2.Text == "Yet to bat")
            {
                switch ((player1Balls - 1) / 6)
                {
                    case 0: lblOver1.Text = lblOver1.Text + ballResult;
                        break;
                    case 1: lblOver2.Text = lblOver2.Text + ballResult;
                        break;
                    case 2: lblOver3.Text = lblOver3.Text + ballResult;
                        break;
                    case 3: lblOver4.Text = lblOver4.Text + ballResult;
                        break;
                    case 4: lblOver5.Text = lblOver5.Text + ballResult;
                        break;
                }
                lblScore1.Text = Convert.ToString(player1Runs) + "/" + Convert.ToString(player1Wkts);
            }
            else
            {
                switch ((player2Balls - 1) / 6)
                {
                    case 0: lblOver1.Text = lblOver1.Text + ballResult;
                        break;
                    case 1: lblOver2.Text = lblOver2.Text + ballResult;
                        break;
                    case 2: lblOver3.Text = lblOver3.Text + ballResult;
                        break;
                    case 3: lblOver4.Text = lblOver4.Text + ballResult;
                        break;
                    case 4: lblOver5.Text = lblOver5.Text + ballResult;
                        break;
                }
                lblScore2.Text = Convert.ToString(player2Runs) + "/" + Convert.ToString(player2Wkts);
            }
            pgLeft.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pgRight.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (player2Runs > player1Runs)
            {
                if (player2Wkts < 4)
                    btnFlip.Content = lblPlayer2.Text + " win by " + Convert.ToString(5 - player2Wkts) + " wickets.\n(" + lblToss.Text + ")";
                else
                    btnFlip.Content = lblPlayer2.Text + " win by " + Convert.ToString(5 - player2Wkts) + " wicket.\n(" + lblToss.Text + ")";
                btnFlip.IsEnabled = false;
                btnToss.IsEnabled = true;
                txtPlayer1.IsEnabled = true;
                txtPlayer2.IsEnabled = true;
                lblToss.Text = "";
                if (lblPlayer2.Text == player1)
                    lblWins1.Content = Convert.ToString(Convert.ToInt16(lblWins1.Content) + 1);
                else
                    lblWins2.Content = Convert.ToString(Convert.ToInt16(lblWins2.Content) + 1);
                btnToss.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
            else if ((player2Balls == 30 || player2Wkts == 5) && player2Runs < player1Runs)
            {
                if (player1Runs - player2Runs > 1)
                    btnFlip.Content = lblPlayer1.Text + " win by " + Convert.ToString(player1Runs - player2Runs) + " runs.\n(" + lblToss.Text + ")";
                else
                    btnFlip.Content = lblPlayer1.Text + " win by " + Convert.ToString(player1Runs - player2Runs) + " run.\n(" + lblToss.Text + ")";
                btnFlip.IsEnabled = false;
                btnToss.IsEnabled = true;
                txtPlayer1.IsEnabled = true;
                txtPlayer2.IsEnabled = true;
                lblToss.Text = "";
                if (lblPlayer1.Text == player1)
                    lblWins1.Content = Convert.ToString(Convert.ToInt16(lblWins1.Content) + 1);
                else
                    lblWins2.Content = Convert.ToString(Convert.ToInt16(lblWins2.Content) + 1);
                btnToss.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
            else if (player2Balls == 30 || player2Wkts == 5)
            {
                btnFlip.Content = "Match tied\n(" + lblToss.Text + ")";
                btnFlip.IsEnabled = false;
                btnToss.IsEnabled = true;
                txtPlayer1.IsEnabled = true;
                txtPlayer2.IsEnabled = true;
                lblToss.Text = "";
                btnToss.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            }
            LineSegment linp = new LineSegment();
            LineSegment linp2 = new LineSegment();
            if (lblScore2.Text == "Yet to bat")
            {
                linp.Point = new Point(player1Balls * 8, 300 - (player1Runs * 2));
                figp.Segments.Add(linp);
                grpRuns1.Data = geop;
                int proj;
                proj = (player1Runs * 30) / player1Balls;
                if (player1Wkts != 0 && proj > (player1Runs * 5) / player1Wkts)
                    proj = (player1Runs * 5) / player1Wkts;
                if (proj > 120)
                    linp2.Point = new Point(player1Balls * 8, 60 - (proj - 120));
                else
                    linp2.Point = new Point(player1Balls * 8, 300 - (proj * 2));
                figp3.StartPoint = new Point(0, linp2.Point.Y);
                figp3.Segments.Add(linp2);
                grpRuns3.Data = geop3;
            }
            else
            {
                linp.Point = new Point(player2Balls * 8, 300 - (player2Runs * 2));
                figp2.Segments.Add(linp);
                grpRuns2.Data = geop2;
                int proj2;
                proj2 = (player2Runs * 30) / player2Balls;
                if (player2Wkts != 0 && proj2 > (player2Runs * 5) / player2Wkts)
                    proj2 = (player2Runs * 5) / player2Wkts;
                if (proj2 > 120)
                    linp2.Point = new Point(player2Balls * 8, 60 - (proj2 - 120));
                else
                    linp2.Point = new Point(player2Balls * 8, 300 - (proj2 * 2));
                figp4.StartPoint = new Point(0, linp2.Point.Y);
                figp4.Segments.Add(linp2);
                grpRuns4.Data = geop4;
            }
            if ((player1Balls == 30 || player1Wkts == 5) && player2Balls == 0)
            {
                lblScore2.Text = "0/0";
            }
        }

        private void chkWorm_Click(object sender, RoutedEventArgs e)
        {
            if (chkWorm.IsChecked == true)
            {
                grpRuns1.StrokeThickness = 1;
                grpRuns1.Fill.Opacity = 0.5;
                grpRuns2.StrokeThickness = 1;
                grpRuns2.Fill.Opacity = 0.5;
            }
            else
            {
                grpRuns1.StrokeThickness = 2;
                grpRuns1.Fill.Opacity = 0;
                grpRuns2.StrokeThickness = 2;
                grpRuns2.Fill.Opacity = 0;
            }
        }

        private void chkProj_Click(object sender, RoutedEventArgs e)
        {
            if (chkProj.IsChecked == true)
            {
                grpRuns3.StrokeThickness = 1;
                grpRuns3.Fill.Opacity = 0.5;
                grpRuns4.StrokeThickness = 1;
                grpRuns4.Fill.Opacity = 0.5;
            }
            else
            {
                grpRuns3.StrokeThickness = 2;
                grpRuns3.Fill.Opacity = 0;
                grpRuns4.StrokeThickness = 2;
                grpRuns4.Fill.Opacity = 0;
            }
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= (4*e.NewSize.Height)/3)
                VisualStateManager.GoToState(this, "CenterState", true);
            else
                VisualStateManager.GoToState(this, "FullLandscape", true);
        }
    }
}
