using System;
using System.Drawing;

namespace DrawingsControllSystem.Model.Common
{
    public class TiffCreatorStrategy : CreateStrategy
    {
        /// <summary>
        /// Описание метода открытия изображения 
        /// </summary>
        protected override void OpenImage()
        {
            try
            {
                bitmap = new Bitmap(file.FullName);
            }
            catch (Exception e)
            {
                bitmap = null;
                throw new Exception(e.Message, e);
            }
        }
    }
}