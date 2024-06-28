
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace N.G.HRS.Repository.File_Upload
{
    public class FileUploadService : IFileUploadService
    {
        public FileUploadService(IHostingEnvironment hostingEnvironment )
        {
            _HostingEnvironment = hostingEnvironment;
        }

        public IHostingEnvironment _HostingEnvironment { get; }

        public async Task<string> UploadFileAsync(IFormFile file, string baseUploadPath)
        {
            // الخطوة 1: التحقق من صحة الملف
            if (file == null || file.Length == 0)
            {
                return "default.jpg";
            }

            // الخطوة 2: إنشاء اسم المجلد باستخدام شهر الآن
            var currentMonthFolder = DateTime.Now.ToString("yyyy-MM");

            // الخطوة 3: دمج مسار التحميل الأساسي مع مجلد الشهر الحالي
            var uploadPath = Path.Combine(_HostingEnvironment.WebRootPath,baseUploadPath, currentMonthFolder);

            // الخطوة 4: التحقق من وجود مجلد الشهر قبل الإنشاء
            if (!Directory.Exists(uploadPath))
            {
                try
                {
                    Directory.CreateDirectory(uploadPath);
                }
                catch (Exception ex)
                {
                    // يمكنك التعامل مع أي استثناء يحدث أثناء الإنشاء هنا
                    throw new ApplicationException("حدث خطأ أثناء إنشاء المجلد", ex);
                }
            }

            // الخطوة 5: إنشاء اسم ملف فريد للملف المحمل
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // الخطوة 6: دمج مسار التحميل مع اسم الملف الفريد
            var filePath = Path.Combine(uploadPath, fileName);

            // الخطوة 7: استخدام FileStream لنسخ محتوى الملف إلى المسار المحدد
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // الخطوة 8: إرجاع المسار النسبي (بما في ذلك الشهر) إلى الملف المحمل
            return Path.Combine(currentMonthFolder, fileName);
        }
    }
}

