//这一整套的语法结构就构成了ShaderLab
Shader "Barry's Shader/first_shader"{//这里制定shader的名字，不要求和文件名保持一致，/就是对应的目录
	Properties{
		//属性
		_Color("Color",Color) = (1,1,1,1) //->float4
		_Vector("Vector",Vector)=(1,2,5,6)//->float4
		_Float("Float",Float)=4.5		  //->float
		_Int("Int",Int)=9				  //->float
		_Range("Range",Range(1,15))=2	  //->float

		//这样设置默认为一张颜色为红色的图片，对于其它两个类型也是如此{}代表制定的颜色
		_2D("Texture",2D) = "Red"{}		  //->sampler2D
		_Cube("Cube",Cube) = "White"{}	  //->samplerCube
		_3D("Texture3D",3D) = "White"{}	  //->sampler3D
	}

		SubShader{
		//至少一个Pass
			Pass{
		//在这里编写Shader代码，当然也可以使用HLSLPROGRAM，GLSLPROGRAM
				CGPROGRAM
				//使用CG语言编写Shader，这一部分是需要分号的，虽然在SubShader外面已经声明了，但是还是需要在PROGRAM中重新写
				//不过这样这些属性的值会和Properties中保持一致
				float4 _Color;
				float4 _Vector;

				float _Range;
				float _Int;
				sampler2D _2D;
				samplerCube _Cube;
				sampler3D _3D;

				ENDCG
				}
	}
		FallBack "VertexLit"
}