using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PiHatWPF.Commands;
using PiHatWPF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PiHatWPF.ViewModel
{
    class ChartsViewModel : BaseViewModel
    {
        #region Properties
        public PlotModel TemperaturePlotModel { get; set; }
        public PlotModel HumidityPlotModel { get; set; }
        public PlotModel PressurePlotModel { get; set; }
        public ConfigButtonCommand StartButton { get; set; }
        public ConfigButtonCommand StopButton { get; set; }
        //public ChartsButtonCommand ClearButton { get; set; }


        #endregion

        #region Fields
        private int timeStamp = 0;
        private ConfigParams Config = new ConfigParams();
        private string ipAdress;
        private Timer RequestTimer;
        private IoTServer Server;
        #endregion

        public ChartsViewModel()
        {
            #region ChartsInitialization
            TemperaturePlotModel = new PlotModel { Title = "Temperature data chart" };
            HumidityPlotModel = new PlotModel { Title = "Humidity data chart" };
            PressurePlotModel = new PlotModel { Title = "Pressure data chart" };

            TemperaturePlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = Config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"



            });

            TemperaturePlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 40,
                Key = "Vertical",
                Unit = "C",
                Title = "Temperature Value"



            });

            TemperaturePlotModel.Series.Add(new LineSeries() { Title = "Temperature data series", Color = OxyColor.Parse("#FFFF0000") });

            HumidityPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = Config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"



            });

            HumidityPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 40,
                Key = "Vertical",
                Unit = "%",
                Title = "Humidity Value"



            });

            HumidityPlotModel.Series.Add(new LineSeries() { Title = "Humidity data series", Color = OxyColor.Parse("#FFFF0000") });

            PressurePlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = Config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"



            });

            PressurePlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 40,
                Key = "Vertical",
                Unit = "hPa",
                Title = "Pressure Value"



            });

            PressurePlotModel.Series.Add(new LineSeries() { Title = "Pressure data series", Color = OxyColor.Parse("#FFFF0000") });
            #endregion

            StartButton = new ConfigButtonCommand(StartTimer);
            StopButton = new ConfigButtonCommand(StopTimer);
            ipAdress = Config.IpAddress;
            Server = new IoTServer(ipAdress);
            
        }
        public void UpdateCharts(double t, double temp, double hum, double press)
        {
            LineSeries TemperatureLineSeries = TemperaturePlotModel.Series[0] as LineSeries;
            LineSeries HumidityLineSeries = HumidityPlotModel.Series[0] as LineSeries;
            LineSeries PressureLineSeries = PressurePlotModel.Series[0] as LineSeries;

            TemperatureLineSeries.Points.Add(new DataPoint(t, temp));
            HumidityLineSeries.Points.Add(new DataPoint(t, hum));
            PressureLineSeries.Points.Add(new DataPoint(t, press));

            if (TemperatureLineSeries.Points.Count > Config.MaxSamples || HumidityLineSeries.Points.Count > Config.MaxSamples || PressureLineSeries.Points.Count > Config.MaxSamples)
            {
                TemperatureLineSeries.Points.RemoveAt(0);
                HumidityLineSeries.Points.RemoveAt(0);
                PressureLineSeries.Points.RemoveAt(0);
            }

            if (t >= Config.XAxisMax)
            {
                TemperaturePlotModel.Axes[0].Minimum = (t - Config.XAxisMax);
                TemperaturePlotModel.Axes[0].Maximum = t + Config.SampleTime / 1000.0;

                HumidityPlotModel.Axes[0].Minimum = (t - Config.XAxisMax);
                HumidityPlotModel.Axes[0].Maximum = t + Config.SampleTime / 1000.0;

                PressurePlotModel.Axes[0].Minimum = (t - Config.XAxisMax);
                PressurePlotModel.Axes[0].Maximum = t + Config.SampleTime / 1000.0;
            }

            TemperaturePlotModel.InvalidatePlot(true);
            HumidityPlotModel.InvalidatePlot(true);
            PressurePlotModel.InvalidatePlot(true);



        }
        private async void UpdatePlotWithServerData()
        {
            string responseText = await Server.GETData();

            try
            {
                dynamic responeJson = JObject.Parse(responseText);
                UpdateCharts(timeStamp / 1000.0, (double)responeJson.temp, (double)responeJson.humi, (double)responeJson.press);

            }
            catch (Exception e)
            {
                Debug.WriteLine("JSON DATA ERROR");
                Debug.WriteLine(responseText);
                Debug.WriteLine(e);


            }
            timeStamp += Config.SampleTime;
        }
        private void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlotWithServerData();
        }

        private void StartTimer()
        {
            if (RequestTimer == null)
            {
                RequestTimer = new Timer(Config.SampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;

                TemperaturePlotModel.ResetAllAxes();
                HumidityPlotModel.ResetAllAxes();
                PressurePlotModel.ResetAllAxes();


            }
        }

        private void StopTimer()
        {
            if (RequestTimer != null)
            {
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
        }
    }
}
