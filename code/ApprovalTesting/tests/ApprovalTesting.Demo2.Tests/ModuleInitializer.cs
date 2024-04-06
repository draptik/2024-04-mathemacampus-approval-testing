using System.Runtime.CompilerServices;

namespace ApprovalTesting.Demo2.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // This forces the usage of VSCode.
        // For details, see: https://github.com/VerifyTests/DiffEngine/blob/main/docs/diff-tool.order.md#via-code
        // DiffTools.UseOrder(DiffTool.VisualStudioCode);

        // To prevent cluttering the main folder, we will collect all verified snapshots in a dedicated folder.
        // For details, see: https://github.com/VerifyTests/Verify/blob/main/docs/naming.md#derivepathinfo
        DerivePathInfo(
            (_, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "VerifiedData"),
                typeName: type.Name,
                methodName: method.Name));

        // Since dotnet rounds floating point numbers differently between Windows and Linux, we will round to 8 digits.
        // This precision is enough for our purposes.
        // VerifierSettings.AddExtraSettings(x => x.FloatPrecision = 8);
    }
}