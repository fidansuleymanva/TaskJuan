namespace Juan.Helpers
{
    public class FileManage
    {
        public static string SaveFile(string rootPath,string folderName,IFormFile formFile)
        {
            string name = formFile.FileName;
            name=name.Length>64? name.Substring(name.Length-64,64):
                name;
            name = Guid.NewGuid().ToString() + name;
            string savePath=Path.Combine(rootPath,folderName,name);
            using (FileStream fs = new FileStream(savePath,FileMode.Create))
            {
                formFile.CopyTo(fs);

            }
            return name;
        }
        public static void DeleteFile(string rootPath, string folderName, string fileName)
        {
            string deletePath = Path.Combine(rootPath,folderName,fileName);
           if(System.IO.File.Exists(deletePath) )
            {
                System.IO.File.Delete(deletePath);                                                                                                  
            }
        }
    }
}
