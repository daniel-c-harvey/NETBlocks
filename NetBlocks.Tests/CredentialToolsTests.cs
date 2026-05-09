using NetBlocks.Utilities.Environment;
using Xunit;

namespace NetBlocks.Tests;

public class CredentialToolsTests : IDisposable
{
    private const string CredentialDirectoryVar = "CREDENTIALS_DIRECTORY";
    private readonly string? _originalCredsDir;

    public CredentialToolsTests()
    {
        // Capture pre-test value so we restore it in Dispose; tests then
        // start from a known-clean state (env var unset).
        _originalCredsDir = System.Environment.GetEnvironmentVariable(CredentialDirectoryVar);
        System.Environment.SetEnvironmentVariable(CredentialDirectoryVar, null);
    }

    public void Dispose()
    {
        System.Environment.SetEnvironmentVariable(CredentialDirectoryVar, _originalCredsDir);
    }

    [Fact]
    public void ResolvePath_EnvVarSet_ReturnsCombinedPath()
    {
        var credsDir = Path.Combine(Path.GetTempPath(), "creds-" + Guid.NewGuid().ToString("N"));
        System.Environment.SetEnvironmentVariable(CredentialDirectoryVar, credsDir);

        var result = CredentialTools.ResolvePath("api-key", "/etc/myapp/api-key");

        Assert.Equal(Path.Combine(credsDir, "api-key"), result);
    }

    [Fact]
    public void ResolvePath_EnvVarNotSet_ReturnsFallbackUnchanged()
    {
        const string fallback = "/etc/myapp/api-key";

        var result = CredentialTools.ResolvePath("api-key", fallback);

        Assert.Equal(fallback, result);
    }

    [Fact]
    public void ResolvePathOrThrow_EnvVarSet_FileExists_ReturnsCombinedPath()
    {
        var credsDir = Path.Combine(Path.GetTempPath(), "creds-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(credsDir);
        var credentialName = "api-key";
        var credentialPath = Path.Combine(credsDir, credentialName);
        File.WriteAllText(credentialPath, "secret-payload");

        try
        {
            System.Environment.SetEnvironmentVariable(CredentialDirectoryVar, credsDir);

            var result = CredentialTools.ResolvePathOrThrow(credentialName, "/nonexistent/fallback");

            Assert.Equal(credentialPath, result);
        }
        finally
        {
            Directory.Delete(credsDir, recursive: true);
        }
    }

    [Fact]
    public void ResolvePathOrThrow_FileNotPresent_ThrowsWithCredentialNameAndPath()
    {
        const string credentialName = "missing-key";
        var fallback = Path.Combine(Path.GetTempPath(), "definitely-not-there-" + Guid.NewGuid().ToString("N"));

        var ex = Assert.Throws<FileNotFoundException>(
            () => CredentialTools.ResolvePathOrThrow(credentialName, fallback));

        Assert.Contains(credentialName, ex.Message);
        Assert.Contains(fallback, ex.Message);
    }
}
