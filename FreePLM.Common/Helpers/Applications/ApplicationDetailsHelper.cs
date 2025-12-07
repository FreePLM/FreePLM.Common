using System.Reflection;

namespace FreePLM.Common.Helpers.Applications
{
    public interface IApplicationDetailsHelper
    {
        string PackageName { get; set; }
        string ApplicationName { get; }
        string ApplicationVersion { get; set; }
    }

    /// <summary>
    /// Helper class that provides application details such as its name and version.
    /// These details are extracted from the application's assembly metadata.
    /// Can be used both as an instance or via static methods.
    /// </summary>
    public class ApplicationDetailsHelper : IApplicationDetailsHelper
    {
        private string? _packageName = null;

        /// <summary>
        /// Gets or sets the package name.
        /// If not set, the name is retrieved from the assembly's <see cref="AssemblyTitleAttribute"/>.
        /// </summary>
        public string PackageName
        {
            get => _packageName ?? _getPackageNameFromAssembly();
            set => _packageName = value;
        }

        private string _applicationName => PackageName.Split('.').Last();

        /// <summary>
        /// Gets the application name, which is the last segment of the period-delimited PackageName.
        /// For example, if PackageName is "Company.Product.Module", this returns "Module".
        /// </summary>
        public string ApplicationName => _applicationName;

        private string? _applicationVersion = null;

        /// <summary>
        /// Gets or sets the application version.
        /// If not set, the version is retrieved from the assembly's <see cref="AssemblyInformationalVersionAttribute"/>.
        /// </summary>
        public string ApplicationVersion
        {
            get => _applicationVersion ?? _getApplicationVersionFromAssembly();
            set => _applicationVersion = value;
        }

        /// <summary>
        /// Retrieves the application name from the assembly's <see cref="AssemblyTitleAttribute"/>.
        /// If the title is not available, returns a default value ("Default Name").
        /// </summary>
        /// <returns>The name of the application.</returns>
        private string _getPackageNameFromAssembly()
        {
            PackageName = GetPackageNameFromAssembly();
            return PackageName;
        }

        /// <summary>
        /// Retrieves the application version from the assembly's <see cref="AssemblyInformationalVersionAttribute"/>.
        /// If the version is in Semantic Versioning format (including build information), only the base version is returned.
        /// If no version is found, returns a default value ("0.0.0").
        /// </summary>
        /// <returns>The version of the application.</returns>
        private string _getApplicationVersionFromAssembly()
        {
            ApplicationVersion = GetApplicationVersionFromAssembly();
            return ApplicationVersion;
        }

        #region Static Methods

        /// <summary>
        /// Static method to retrieve the package name from the assembly's <see cref="AssemblyTitleAttribute"/>.
        /// If the title is not available, returns a default value ("Default Name").
        /// </summary>
        /// <returns>The package name of the application.</returns>
        public static string GetPackageName()
        {
            return GetPackageNameFromAssembly();
        }

        /// <summary>
        /// Static method to retrieve the application name, which is the last segment of the period-delimited package name.
        /// For example, if PackageName is "Company.Product.Module", this returns "Module".
        /// </summary>
        /// <returns>The application name.</returns>
        public static string GetApplicationName()
        {
            var packageName = GetPackageNameFromAssembly();
            return packageName.Split('.').Last();
        }

        /// <summary>
        /// Static method to retrieve the application version from the assembly's <see cref="AssemblyInformationalVersionAttribute"/>.
        /// If the version is in Semantic Versioning format (including build information), only the base version is returned.
        /// If no version is found, returns a default value ("0.0.0").
        /// </summary>
        /// <returns>The version of the application.</returns>
        public static string GetApplicationVersion()
        {
            return GetApplicationVersionFromAssembly();
        }

        #endregion

        #region Private Static Helper Methods

        /// <summary>
        /// Internal helper to retrieve package name from assembly.
        /// </summary>
        private static string GetPackageNameFromAssembly()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            return titleAttribute?.Title ?? "Default Name";
        }

        /// <summary>
        /// Internal helper to retrieve version from assembly.
        /// </summary>
        private static string GetApplicationVersionFromAssembly()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var versionAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var version = versionAttribute?.InformationalVersion ?? "0.0.0";
            
            // If the version is returned in Semantic Versioning (SemVer) format with build metadata (e.g. 1.0.0+build123),
            // split off the base version to return only "1.0.0".
            var baseVersion = version.Split('+')[0];
            return baseVersion;
        }

        #endregion
    }
}