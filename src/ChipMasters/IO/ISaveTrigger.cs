using System.Threading.Tasks;

namespace ChipMasters.IO
{
    /// <summary>
    /// Contract for an object that coordinates saving some data to a set file.
    /// </summary>
    public interface ISaveTrigger
    {
        /// <summary>
        /// Create a background save operation. If background save is ongoing, should complete syncronously then proceed.
        /// </summary>
        void Save();

        /// <summary>
        /// Save the file and prevent further operations.
        /// </summary>
        Task Close();
    } // end interface
} // end namespace