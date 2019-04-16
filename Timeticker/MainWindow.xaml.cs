using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Timeticker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(TimeCycle);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void TimeCycle(object sender, EventArgs e)
        {
            event_Grid.ItemsSource = null;
            var events = EventHelper.GetEventModels();
            event_Grid.ItemsSource = events;

            var dt = DateTime.Now;
            for(int i=0;i< events.Count; i++)
            {
                if (events[i].EventTime.Subtract(dt) < TimeSpan.FromSeconds(8) && !events[i].IsNotify)
                {
                    NotifyData data = new NotifyData();
                    data.Content = events[i].Remark;

                    NotificationWindow dialog = new NotificationWindow();//new 一个通知
                    dialog.Closed += Dialog_Closed;
                    dialog.TopFrom = GetTopFrom();
                    _dialogs.Add(dialog);
                    events[i].IsNotify = true;
                    dialog.DataContext = data;//设置通知里要显示的数据
                    dialog.Show();
                }
            }
        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind)
            {
                topFrom = topFrom - 110;//此处100是NotifyWindow的高 110-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotificationWindow;
            _dialogs.Remove(closedDialog);
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(event_Time.Text) || string.IsNullOrWhiteSpace(event_Remark.Text))
            {
                return;
            }

            EventHelper.AddEvent(new EventModel { EventTime = TimerHelper.ConvertEventTime(event_Time.Text.Trim()), Remark = event_Remark.Text.Trim() });
        }
    }
}
