using _6HET_ih3k69_5fel.Entities;
using _6HET_ih3k69_5fel.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            comboBox.Text = "EUR";
            var mnbService = new MNBArfolyamServiceSoapClient();
            var rek = new GetCurrenciesRequestBody();
            var válasz = mnbService.GetCurrencies(rek);
            var eredmény = válasz.GetCurrenciesResult;
            var xml2 = new XmlDocument();
            xml2.LoadXml(eredmény);
            foreach (XmlElement element in xml2.DocumentElement)
            {
                foreach (var item in element.ChildNodes)
                {
                    Currencies.Add(((XmlElement)item).InnerText);
                }
                
            }
            comboBox.DataSource = Currencies;
            RefreshData();

        }

        private void RefreshData()
        {
            Rates.Clear();
            
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker2_ValueChanged;
            comboBox.Click += ComboBox_Click;
            xmlfeladat();
            adatok();
            dataGridView1.DataSource = Rates;

        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
       
       private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }      
        private void ComboBox_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        string webszol()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox.SelectedItem.ToString(),
                startDate=dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()

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
                var rate = new RateData();
                Rates.Add(rate);
                //dátum
                    rate.Date = DateTime.Parse(element.GetAttribute("date"));
                //valuta
                    var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");
                //érték
                    var unit = decimal.Parse(childElement.GetAttribute("unit"));
                    var value = decimal.Parse(childElement.InnerText);
                    if (unit != 0)
                    rate.Value = value / unit;
            }
        }
        void adatok()
        {
            chartRatesData.DataSource = Rates;
            var series = chartRatesData.Series[0];
            series.ChartType=SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRatesData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRatesData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }

        private void chartRatesData_Click(object sender, EventArgs e)
        {

        }
    }
}
