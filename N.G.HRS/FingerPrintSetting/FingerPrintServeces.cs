using BioMetrixCore;
using OfficeOpenXml;

namespace N.G.HRS.FingerPrintSetting
{
    internal class FingerPrintServeces
    {

        DeviceManipulator manipulator = new DeviceManipulator();
        ZkemClient objZkeeper = new ZkemClient(RaiseDeviceEvent);
        private bool isDeviceConnected = false;
        //public FingerPrintServeces(ZkemClient _objZkeeper)
        //{
        //    objZkeeper = _objZkeeper;
        //}
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set
            {
                isDeviceConnected = value;
                if (isDeviceConnected)
                {
                    throw new NotImplementedException("تم الاتصال بنجاح");
                }
                else
                {
                    objZkeeper.Disconnect();
                    throw new NotImplementedException("تم قطع الاتصال بنجاح");
                }
            }
        }

        public ICollection<UserInfo> GetAllUserData(int machinrNo)
        {

            ICollection<UserInfo> lstFingerPrintTemplates = manipulator.GetAllUserInfo(objZkeeper, machinrNo);
            if (lstFingerPrintTemplates != null && lstFingerPrintTemplates.Count > 0)
            {
                return lstFingerPrintTemplates;
            }
            else
                return new List<UserInfo>();

        }
        public ICollection<UserInfo> GetAllUserId(int machinrNo)
        {
            ICollection<UserIDInfo> lstUserIDInfo = manipulator.GetAllUserID(objZkeeper, machinrNo);
            if (lstUserIDInfo != null && lstUserIDInfo.Count > 0)
            {
                return lstUserIDInfo.Cast<UserInfo>().ToList();
            }
            else
            {
                return new List<UserInfo>();
            }
        }
        //==============================GET DATA BETWEEN TWO DATE=============================
        public ICollection<UserInfo> GetDataBetweenDates(int machinrNo, DateTime startDate, DateTime endDate)
        {
            ICollection<UserIDInfo> lstUserIDInfo = manipulator.GetAllUserID(objZkeeper, machinrNo);
            if (lstUserIDInfo != null && lstUserIDInfo.Count > 0)
            {
                return lstUserIDInfo.Where(user => user.Date >= startDate && user.Date <= endDate).Cast<UserInfo>().ToList();
            }
            else
            {
                return new List<UserInfo>();
            }
        }
        public ICollection<UserInfo> GetDataBetweenDates(int machinrNo, DateTime startDate)
        {
            ICollection<UserIDInfo> lstUserIDInfo = manipulator.GetAllUserID(objZkeeper, machinrNo);
            if (lstUserIDInfo != null && lstUserIDInfo.Count > 0)
            {
                return lstUserIDInfo.Where(user => user.Date >= startDate).Cast<UserInfo>().ToList();
            }
            else
            {
                return new List<UserInfo>();
            }
        }
        //==============================--******************************************--=============================
        //==============================GET DATA BY EMPLOYEE NUMBER=============================    
        public IEnumerable<UserInfo> GetDataByEmployeeNumber(string employeeNumber, int machinrNo)
        {
            // Retrieve all user data from the data source
            ICollection<UserInfo> allUserData = GetAllUserData(machinrNo);
            // Find the user data based on the employee number
            if (allUserData != null && allUserData.Count > 0)

                return allUserData.Where(user => user.EnrollNumber == employeeNumber).ToList();
            else
                return new List<UserInfo>();
        }
        public IEnumerable<UserInfo> GetDataByEmployeeNumber(string employeeNumber, int machinrNo, DateTime startDate, DateTime endDate)
        {
            // Retrieve all user data from the data source
            ICollection<UserInfo> allUserData = GetAllUserData(machinrNo);
            // Find the user data based on the employee number
            if (allUserData != null && allUserData.Count > 0)

                return allUserData.Where(user => user.EnrollNumber == employeeNumber && user.Date >= startDate && user.Date <= endDate).ToList();
            else
                return new List<UserInfo>();
        }
        public IEnumerable<UserInfo> GetDataByEmployeeNumber(string employeeNumber, int machinrNo, DateTime startDate)
        {
            // Retrieve all user data from the data source
            ICollection<UserInfo> allUserData = GetAllUserData(machinrNo);
            // Find the user data based on the employee number
            if (allUserData != null && allUserData.Count > 0)

                return allUserData.Where(user => user.EnrollNumber == employeeNumber && user.Date >= startDate);
            else
                return new List<UserInfo>();

        }
        //==============================--******************************************--=============================
        public void UploadUsersFromExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var startRow = 2; // Assuming the data starts from row 2
                for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                {
                    var MachineNumber = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                    var enrollNumber = int.Parse(worksheet.Cells[row, 2].Value.ToString());
                    var userName = worksheet.Cells[row, 3].Value.ToString();
                    var Password = worksheet.Cells[row, 4].Value.ToString();
                    var Privelage = int.Parse(worksheet.Cells[row, 5].Value.ToString());
                    var Enabled = bool.Parse(worksheet.Cells[row, 6].Value.ToString());
                    // Additional fields can be extracted and used as needed
                    // Upload the user info to the fingerprint device
                    objZkeeper.SetUserInfo(MachineNumber, enrollNumber, userName, Password, Privelage, Enabled);
                }
            }
        }
        //==============================--******************************************--=============================
        public void UploadUsersFromTextFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split('\t');

                    var MachineNumber = int.Parse(fields[0]);
                    var enrollNumber = int.Parse(fields[1]);
                    var userName = fields[2];
                    var Password = fields[3];
                    var Privelage = int.Parse(fields[4]);
                    var Enabled = bool.Parse(fields[5]);

                    // Additional fields can be extracted and used as needed

                    // Upload the user info to the fingerprint device
                    objZkeeper.SetUserInfo(MachineNumber, enrollNumber, userName, Password, Privelage, Enabled);
                }
            }
        }
        //==============================--******************************************--=============================
        public void SaveUserInfoToExcelFile(string filePath,int machineNo)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("UserInfo");

                // Add headers
                worksheet.Cells[1, 1].Value = "MachineNumber";
                worksheet.Cells[1, 2].Value = "EnrollNumber";
                worksheet.Cells[1, 3].Value = "UserName";
                worksheet.Cells[1, 4].Value = "Password";
                worksheet.Cells[1, 5].Value = "Privelage";
                worksheet.Cells[1, 6].Value = "Enabled";

                // Get the user information from the fingerprint device
                var userInfo = GetAllUserData(machineNo);

                // Add data rows to the worksheet
                int row = 2;
                foreach (var info in userInfo)
                {
                    worksheet.Cells[row, 1].Value = info.MachineNumber;
                    worksheet.Cells[row, 2].Value = info.EnrollNumber;
                    worksheet.Cells[row, 3].Value = info.Name;
                    worksheet.Cells[row, 4].Value = info.Password;
                    worksheet.Cells[row, 5].Value = info.Privelage;
                    worksheet.Cells[row, 6].Value = info.Enabled;

                    row++;
                }

                // Save the Excel file
                package.SaveAs(new FileInfo(filePath));
            }
        }
        //=====================================================
        public void SaveUserInfoToTextFile(string filePath, int machineNo)
        {
            using (var writer = new StreamWriter(filePath))
            {
                // Write headers
                writer.WriteLine("MachineNumber\tEnrollNumber\tUserName\tPassword\tPrivelage\tEnabled");

                // Get the user information from the fingerprint device
                var userInfo = GetAllUserData(machineNo);

                // Write data rows to the text file
                foreach (var info in userInfo)
                {
                    writer.WriteLine($"{info.MachineNumber}\t{info.EnrollNumber}\t{info.Name}\t{info.Password}\t{info.Privelage}\t{info.Enabled}");
                }
            }
        }
        //public void ImportFromExcel(string filePath)
        //{
        //    using (var package = new ExcelPackage(new FileInfo(filePath)))
        //    {
        //        var worksheet = package.Workbook.Worksheets["UserData"];

        //        // Read the data from the worksheet
        //        var userData = new List<UserInfo>();
        //        for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
        //        {
        //            var id = worksheet.Cells[i, 1].Value?.ToString();
        //            var name = worksheet.Cells[i, 2].Value?.ToString();
        //            var email = worksheet.Cells[i, 3].Value?.ToString();

        //            userData.Add(new UserInfo { Id = id, Name = name, Email = email });
        //        }

        //        // Send the user data to the fingerprint device
        //        SendToFingerprintDevice(userData);
        //    }
        //}
        ////==============================--******************************************--=============================

        private static void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        //Raise Disconnected Event
                        throw new NotImplementedException(" الجهاز غير  متصل 😴");
                    }

                default:
                    break;
            }

        }
    }
}
