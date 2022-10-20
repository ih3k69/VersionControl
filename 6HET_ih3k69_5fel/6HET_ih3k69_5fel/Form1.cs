using _6HET_ih3k69_5fel.Entities;
using _6HET_ih3k69_5fel.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _6HET_ih3k69_5fel
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates;
            xmlfeladat();
           adatok();
           
        }
        string webszol()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            return result;
        }
        void xmlfeladat()
        {
            var xml = new XmlDocument();
            xml.LoadXml(webszol());
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rd = new RateData();
                Rates.Add(rd);
                
                    rd.Date = DateTime.Parse(element.GetAttribute("date"));
                    var childElement = (XmlElement)element.ChildNodes[0];
                    rd.Currency = childElement.GetAttribute("curr");
                    var unit = decimal.Parse(childElement.GetAttribute("unit"));
                    var value = decimal.Parse(childElement.InnerText);
                    if (unit != 0)
                    rd.Value = value / unit;
            }
        }
        void adatok()
        {
            chartRatesData.DataSource = Rates;
            var series = chartRatesData.Series[0];
            series.ChartType=SeriesChartType.Line;
           // series.XValueMember = "Data";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRatesData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRatesData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }

    }
}
