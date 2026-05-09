namespace NetBlocks.Utilities.Environment;

public static class CredentialTools
{
    private const string CredentialDirectoryVar = "CREDENTIALS_DIRECTORY";

    /// <summary>
    /// Resolves the path to a credential file.
    /// If CREDENTIALS_DIRECTORY is set (i.e. running under systemd with
    /// LoadCredentialEncrypted=), returns {CREDENTIALS_DIRECTORY}/{credentialName}.
    /// Otherwise returns fallbackPath unchanged (local dev / non-systemd deployments).
    /// </summary>
    public static string ResolvePath(string credentialName, string fallbackPath)
    {
        var credsDir = System.Environment.GetEnvironmentVariable(CredentialDirectoryVar);
        return credsDir is not null
            ? Path.Combine(credsDir, credentialName)
            : fallbackPath;
    }

    /// <summary>
    /// As ResolvePath, but throws FileNotFoundException if the resolved path
    /// does not exist on disk. Use in services where a missing secret should
    /// fail startup loudly rather than propagate a confusing downstream error.
    /// </summary>
    public static string ResolvePathOrThrow(string credentialName, string fallbackPath)
    {
        var path = ResolvePath(credentialName, fallbackPath);
        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Credential file not found. Credential name: '{credentialName}', resolved path: '{path}'.", path);
        return path;
    }
}
