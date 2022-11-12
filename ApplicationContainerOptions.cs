namespace DIClassLibrary
{
    public class ApplicationContainerOptions
    {
        /// <summary>
        /// Command line arguments.
        /// </summary>
        public string[] Args { get; init; }

        /// <summary>
        /// The environment name.
        /// </summary>
        public string EnvironmentName { get; init; }

        /// <summary>
        /// The application name.
        /// </summary>
        public string ApplicationName { get; init; }
    }
}