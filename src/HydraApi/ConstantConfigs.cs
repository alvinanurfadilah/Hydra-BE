namespace HydraApi;

public static class ConstantConfigs
{
    public static string STATUS_OK = "OK";
    public static string STATUS_FAILED = "FAILED";
    public static string STATUS_NOT_FOUND = "NOT FOUND";
    public static string MESSAGE_FAILED = "Ada kesalahan pada sistem";
    public static string MESSAGE_GET(string rep = "") => $"Data {rep} berhasil didapatkan";
    public static string MESSAGE_POST(string rep = "") => $"Data {rep} berhasil disimpan";
    public static string MESSAGE_PUT(string rep = "") => $"Data {rep} berhasil diubah";
    public static string MESSAGE_DELETE(string rep = "") => $"Data {rep} berhasil dihapus";
    public static string MESSAGE_NOT_FOUND(string rep = "") => $"Data {rep} masih kosong";
}