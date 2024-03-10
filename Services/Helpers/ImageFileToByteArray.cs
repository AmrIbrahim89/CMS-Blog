using Microsoft.AspNetCore.Http;

namespace Services.Helpers
{
    public static class ImageFileToByteArray
    {
        public static async Task<byte[]> Convert(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                throw new ArgumentException(nameof(file));
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var data = stream.ToArray();

                return data;
            }
        }
    }
}
