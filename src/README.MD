# OpenCV 以及 Aries 平台

>### 目录
>1. 图像入门
>2. Aries平台
>3. 图像处理
>4. 图像变换
>5. 轮廓
>6. 图像局部与分割
>7. 跟踪与运动
>8. 摄像机标定
>9. 机器学习


### 图像入门

>#### 成像原理
> ![](https://img-blog.csdnimg.cn/20190423175749753.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MTQ0NTM4Nw==,size_16,color_FFFFFF,t_70)


>#### 图像的颜色
>- 色度学理论认为，任何颜色可由红、绿、蓝三种基本颜色混合得到。  图像可用红、绿、蓝三原色来表示。
>- 计算机屏幕上显示出来的画面通常有两种描述方法：一种为图形，另一种为图像
>图形：
>由指令集合组成; 指令由位置、形状、颜色等描述。 记录的是坐标值; 颜色隐含,统一描述。 显示时执行命令，转变为屏幕上所显示的形状和颜色

>#### 图像：
>光度值(亮度或彩色)； 位置按规则方式排列； 坐标值隐含。
> 二维图像由一个数组或矩阵表示。  
![2](https://upload-images.jianshu.io/upload_images/19391340-0ee704c1ccafaf07.png)

> ##### 彩色图像
![](https://img-blog.csdnimg.cn/20190423181659582.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MTQ0NTM4Nw==,size_16,color_FFFFFF,t_70)

> ##### 8位伪彩色索引图像
>1. 颜色表红、绿、蓝分量值不全相等。
>2. 像素值是图像颜色表的索引地址。  

> ##### 真彩色图像
>1. 每一像素由RGB三个分量组成。
>2. 2.每个分量各占8位，取值范围为0~255，每个像素24位

>#### 图像空间分辨率
>![1](https://img-blog.csdnimg.cn/2019042318111189.PNG?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MTQ0NTM4Nw==,size_16,color_FFFFFF,t_70) 
>指图像数字化的空间精细程度。

>#### 灰度级分辨率： 
>![2](https://img-blog.csdnimg.cn/20190423181116426.PNG?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MTQ0NTM4Nw==,size_16,color_FFFFFF,t_70)
>即颜色深度，表示每一像素的颜色值所占的二进制位数。颜色深度越大则能表示的颜色数目越多。

>#### 图像处理的目的
>1. 提高图像的视感质量，达到赏心悦目的目的。 图像去除噪声，改变图像的亮度、颜色，增强图像中的某些成份、抑制某些成份，对图像进行几何变换等，从而改善图像的质量。
>2. 提取图像中某些特征， 以便于分析。     常用作模式识别、计算机视觉的预处理等。这些特征包括很多方面，如频域特性、灰度／颜色特性、边界／区域特性、纹理特性、形状特性等。
>3. 图像识别     在分析的基础上，进行内容识别，例如：汽车牌照识别，人脸识别、虹膜识别、指纹识别等。
>4. 对图像数据压缩，便于存储和传输。     提高存储量，提高网络的速度。

### Aries平台

### 图像处理
 - 图像感兴趣区域
 - 拆分和合并通道
 - 为图像设置边框
 - 算数运算
 - 形态学
    - 腐蚀
    - 膨胀  
    - 开
    - 闭
    - 礼帽
    - 黑帽 
 - 改变颜色空间
 - 几何变换
 - 阈值
 - 平滑

### 图像变换
 - 卷积
 - Canny
 - 霍夫变换
     - 霍夫线变换
     - 霍夫圆变换
 - 拉伸，收缩，扭曲，旋转
 - 积分
 - 距离变换

### 轮廓

### 分割