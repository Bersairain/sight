using HalconControl;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewWindow.SupportROIModel;

namespace Sight.command
{
    public class KeyMatch: Icommand
    {
        // 搜索绘制区域
        List<ViewWindow.Model.ROI> SearchDrawRegion;
        /// <summary>
        /// 搜索区域
        /// </summary>
        public ROI SearchRegion;
        public ROI ModelRegion;
        public int Flag_Model { get; private set; }

        public void selectRoi(HWindow_Final hWindow_Final)
        {
            try
            {
                // 绘制搜索区域模式
                 Flag_Model = 1;
                // 绘制搜索区域
                hWindow_Final.viewWindow.genRect1(100.0, 100.0, 200.0, 200.0, ref this.SearchDrawRegion);
                // 1.搜索区域  
                //List<double> Seach_data = new List<double>();
                //// 被选中roi索引
                //int index = -1;
                //// 2.获取当前选中的ROI区域
                //hWindow_Final.viewWindow.smallestActiveROI(out Seach_data, out index);

                //// 3.功能调用
                //if (index >= 0)
                //{
                //    // 搜索区域
                //    if (Flag_Model == 1)
                //    {
                //        SearchRegion = new Rectangle_INFO(Seach_data[0], Seach_data[1], Seach_data[2], Seach_data[3]);

                //    }
                //    // 模板区域制作
                //    if (Flag_Model == 2)
                //    {
                //        //ModelRegion = new Rectangle_INFO(Seach_data[0], Seach_data[1], Seach_data[2], Seach_data[3]);

                //    }

                //}
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }
        public void setsearcharea(HWindow_Final hWindow_Final)
        {
            // 1.搜索区域  
            List<double> Seach_data = new List<double>();
            // 被选中roi索引
            int index = -1;
            // 2.获取当前选中的ROI区域
            hWindow_Final.viewWindow.smallestActiveROI(out Seach_data, out index);

            // 3.功能调用
            if (index >= 0)
            {
                // 搜索区域

                    SearchRegion = new Rectangle_INFO(Seach_data[0], Seach_data[1], Seach_data[2], Seach_data[3]);


                // 模板区域制作



            }
        }


        private void setmodlearea(HWindow_Final hWindow_Final,HObject CurrImage)
        {
            // 1.搜索区域  
            List<double> Seach_data = new List<double>();
            // 被选中roi索引
            int index = -1;
            // 2.获取当前选中的ROI区域
            hWindow_Final.viewWindow.smallestActiveROI(out Seach_data, out index);

            // 3.功能调用
            if (index >= 0)
            {
                // 搜索区域

                // 模板区域制作

                    ModelRegion = new Rectangle_INFO(Seach_data[0], Seach_data[1], Seach_data[2], Seach_data[3]);



            }
            // 获取模板图像
            HOperatorSet.ReduceDomain(CurrImage, ModelRegion.GenRegion(), out HObject ModelImage);

            // 创建模板
            HOperatorSet.CreateShapeModel(ModelImage, 0, -3.14, 6.28, "auto", "auto", "use_polarity", 30, "auto", out HTuple ModelId);

            // 保存模板
            HOperatorSet.WriteShapeModel(ModelId, "ShapeModel.shm");


            //// 获取搜索区域图像
            //HOperatorSet.ReduceDomain(CurrImage, SearchRegion.GenRegion(), out HObject SearchImage);
            //// 模板匹配
            //HOperatorSet.FindShapeModel(SearchImage, ModelId, -3.14, 6.28, 0.5, 1, 0.5, "least_squares", 0, 0.5, out HTuple row, out HTuple column, out HTuple angle, out HTuple score);

            //// 显示匹配结果
            //// 刷新当前显示控件
            //hWindow_Final.HobjectToHimage(CurrImage);
            //// 生成十字叉
            //HOperatorSet.GenCrossContourXld(out HObject Cross, row, column, 60, 0);

            //// 显示十字叉
            //hWindow_Final.DispObj(Cross, "blue");

            MessageBox.Show("保存成功");

        }

        //private void keymatch()
        //{
        //    // 1.读取模板图像
        //    // 2.读取模板区域
        //    // 3.获取模板图像
        //    // 4.创建模板
        //    // 5.获取搜索区域图像
        //    // 6.模板匹配
        //    // 7.显示匹配结果
        //    // 7.1刷新当前显示控件
        //    // 7.2获取模板形状
        //    // 7.3获取仿射矩阵
        //    // 7.4对轮廓进行仿射变换
        //    // 7.5显示轮廓
        //    deleteModelFlag = true;


        //    // 1.读取模板图像
        //    HOperatorSet.ReadImage(out HObject ModelImagre, @"C:\Users\Administrator\source\repos\Sight\Sight\bin\Debug\ShapeModel.shm");

        //    // 2.读取模板区域
        //    HOperatorSet.ReadRegion(out HObject paint_region_final, @"D:\\paint_region_final.reg");

        //    // 3.获取模板图像
        //    HOperatorSet.ReduceDomain(ModelImagre, paint_region_final, out HObject RealModelImage);

        //    // 4.创建模板
        //    HOperatorSet.CreateShapeModel(RealModelImage, 0, -3.14, 6.28, "auto", "auto", "use_polarity", 30, "auto", out HTuple ModelId);
        //    // 5.获取搜索区域图像
        //    HOperatorSet.ReduceDomain(CurrImage, SearchRegion, out HObject SeatchImage);
        //    // 6.模板匹配
        //    HOperatorSet.FindShapeModel(SeatchImage, ModelId, -3.14, 6.28, 0.5, 1, 0.5, "least_squares", 0, 0.5, out HTuple row, out HTuple column, out HTuple angle, out HTuple score);

        //    // 7.显示匹配结果

        //    // 7.1刷新当前显示控件
        //    hWindow_Final1.HobjectToHimage(SeatchImage);

        //    // 7.2获取模板形状
        //    HOperatorSet.GetShapeModelContours(out HObject modelxld, ModelId, 1);

        //    // 7.3获取仿射矩阵
        //    HOperatorSet.VectorAngleToRigid(0, 0, 0, row, column, angle, out HTuple homMat2D);

        //    // 7.4对轮廓进行仿射变换
        //    HOperatorSet.AffineTransContourXld(modelxld, out HObject modelXldAfter, homMat2D);

        //    // 7.5显示轮廓
        //    hWindow_Final1.DispObj(modelXldAfter, "red");

        //}
        public void setpara(HWindow_Final hWindow_Final)
        {
            selectRoi(hWindow_Final);
        }

        public void start(HWindow_Final hWindow_Final, HObject CurrImage,string mode)
        {
            switch (mode)
            {
                case "1":
                    setsearcharea(hWindow_Final);
                    break;
                case "2":
                    setmodlearea(hWindow_Final, CurrImage);
                    break;
                default:
                    break;
            }

        }

        public void match(HWindow_Final hWindow_final)
        {
            throw new NotImplementedException();
        }
    }
}