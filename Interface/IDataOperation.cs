using System;
using System.Collections.Generic;
using TaskWPF.Dto;

namespace TaskWPF.Interface
{
    /// <summary>
    /// Interface for data operation operations.
    /// </summary>
    public interface IDataOperation
    {
        /// <summary>
        /// Loads JSON data from a file and deserializes it into ApplicationData object.
        /// </summary>
        /// <param name="filePath">Path to the JSON file.</param>
        /// <returns>Deserialized ApplicationData object.</returns>
        ApplicationData LoadJsonData(string filePath);

        /// <summary>
        /// Filters out noisy areas based on SNR and speed thresholds.
        /// </summary>
        /// <param name="distances">List of distances.</param>
        /// <param name="SNRThreshold">Signal-to-noise ratio threshold.</param>
        /// <param name="SpeedThreshold">Speed threshold.</param>
        /// <returns>List of clean measurement areas represented as tuples of (start distance, end distance, speed).</returns>
        List<Tuple<double, double, double>> FilterNoisyAreas(IList<Distances> distances, double SNRThreshold, double SpeedThreshold);
    }
}