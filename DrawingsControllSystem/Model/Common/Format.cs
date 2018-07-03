namespace DrawingsControllSystem.Model.Common
{
    /// <summary>
    /// Information for format
    /// </summary>
    public class Format
    {
        /// <summary>
        /// Gets format name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets format width
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Gets format height
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Gets format area
        /// </summary>
        public int Area { get; }

        /// <summary>
        /// Gets a low level of the area for the format
        /// </summary>
        public int LowArea { get; }

        /// <summary>
        /// Gets a up level of the area for the format
        /// </summary>
        public int UpArea { get; }

        /// <summary>
        /// Gets value includes standart formats (a4)
        /// </summary>
        public double Includes { get; }

        /// <summary>
        /// Inizialize new format object
        /// </summary>
        /// <param name="name">Format name</param>
        /// <param name="width">Format width</param>
        /// <param name="height">Format height</param>
        /// <param name="area">Format area</param>
        /// <param name="lowArea">Low level of the area</param>
        /// <param name="upArea">Up level of the area</param>
        /// <param name="includes">Includes standart formats</param>
        public Format(string name, double width, double height, int area, int lowArea, int upArea, double includes)
        {
            Name = name;
            Width = width;
            Height = height;
            Area = area;
            LowArea = lowArea;
            UpArea = upArea;
            Includes = includes;
        }
    }
}