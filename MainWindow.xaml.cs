using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;
using OxyPlot.Wpf;
using TaskWPF.Dto;
using TaskWPF.Interface;
using TaskWPF.concrete;
using Microsoft.Win32;

namespace TaskWPF
{
    // Define your classes for deserialization

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataOperation operation = new DataOperation();

        public MainWindow()
        {
            InitializeComponent();

            // Load data from JSON file
            string jsonFilePath = "Jsons/SlabMeasure0132981.json";
            ApplicationData applicationData = operation.LoadJsonData(jsonFilePath);

            // Filter out noisy areas
            List<Tuple<double, double, double>> cleanMeasurementAreas = operation.FilterNoisyAreas(applicationData.Distances, applicationData.SNRThreshold, applicationData.SpeedThreshold);

            // Create and populate the plot model
            var plotModel = new PlotModel { Title = "Данные измерений" };

            // Set the plot model to the plot view
            plotView.Model = CreateModel(plotModel, cleanMeasurementAreas);
        }

        /// <summary>
        /// Creates the OxyPlot model for visualizing clean measurement areas.
        /// </summary>
        /// <param name="plotModel">The base plot model.</param>
        /// <param name="cleanMeasurementAreas">The list of clean measurement areas.</param>
        /// <returns>The populated OxyPlot model.</returns>
        public PlotModel CreateModel(PlotModel? plotModel, List<Tuple<double, double, double>> cleanMeasurementAreas)
        {
            var lineSeries = new LineSeries { Title = "Измерения" };

            // Add points to the line series for clean measurement areas
            foreach (var area in cleanMeasurementAreas)
            {
                lineSeries.Points.Add(new DataPoint(area.Item1, area.Item3)); // Start of area
                lineSeries.Points.Add(new DataPoint(area.Item2, area.Item3)); // End of area
            }
            plotModel.Series.Add(lineSeries);

            // Add arrows to highlight clean measurement areas
            foreach (var area in cleanMeasurementAreas)
            {
                var arrowAnnotation = new ArrowAnnotation
                {
                    StartPoint = new DataPoint(area.Item1, area.Item3),
                    EndPoint = new DataPoint(area.Item2, area.Item3),
                    Color = OxyColors.Red,
                    HeadLength = 5,
                    HeadWidth = 5
                };
                plotModel.Annotations.Add(arrowAnnotation);
            }
            return plotModel;
        }
    }
}