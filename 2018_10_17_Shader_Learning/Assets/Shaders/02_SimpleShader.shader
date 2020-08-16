Shader "Barry's Shader/SimpleShader"
{
	Properties
	{

	}

	SubShader{
		Pass
		{
		CGPROGRAM
		//顶点函数，这里只是用来声明了顶点函数的函数名
		//基本作用是完成模型空间到屏幕视野(剪裁空间)的转换
#pragma vertex vert
		//片元函数，这里只是用来声明了片元函数的函数名
		//返回每一个对应屏幕像素的颜色值
#pragma fragment frag
		xxx vert(xxx)
		{}

		xxxx frag(xxxx)
		{}
		ENDCG
		}

	}
	
	FallBack "VertexLit"
}