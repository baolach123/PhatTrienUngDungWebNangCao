using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;


namespace TatBlog.Menu
{
    public class Menu
    {
        public void XuatMenu()
        {
            Console.WriteLine("---------Menu--------");
            Console.WriteLine("0.Thoat");
            Console.WriteLine("1.Tim mot the theo ten dind danh");
            Console.WriteLine("2.Lay tat ca cac the kem theo so bai viet chua the do");
            Console.WriteLine("3.Xoa mot ma the cho truoc");
            Console.WriteLine("4.Tim mot chuyen muc theo ten dinh danh");
            Console.WriteLine("5.Tim mot chuyen muc theo ma so cho truoc");
            Console.WriteLine("6.Them hoac cap nhat mot chuyen muc/ chu de");
            Console.WriteLine("7.Xoa mot chuyen muc theo ma so cho truoc");
            Console.WriteLine("8.Kiem tra ten dinh danh cua mot chuyen muc da ton tai hay chua");
            Console.WriteLine("9.Lay va phan trang danh sach chuyen muc");
            Console.WriteLine("10.Dem so luong bai viet trong N thang gan nhat");
            Console.WriteLine("11.Tim mot bai viet theo ma so");
            Console.WriteLine("12.Them hay cap nhat mot so bai viet");
        }

        public int ChonMenu()
        {
            int n;
            Console.WriteLine("Chon chuc nang:");
            n = Convert.ToInt32(Console.ReadLine());
            if (n > 0 || n < 20)
                return n;
            else return ChonMenu();
        }

        void XyLyMenu()
        {
            var context = new BlogDbContext();

            IBlogRepository blogRepo = new BlogRepository(context);
            
            int options = ChonMenu();
            switch (options)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Tim mot the theo ten dind danh:");
                    var tagf = await blogRepo.SeekTagWithUrlslugAync("ADuGoogle");
                    Console.WriteLine(tagf.Name + "");
                    break;




            }
        }
    }

}
