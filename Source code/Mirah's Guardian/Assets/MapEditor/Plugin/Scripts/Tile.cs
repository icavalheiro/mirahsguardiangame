using UnityEngine;
using System;

//Classe eh basicamente uma Struct
[System.Serializable]
public class Tile
{
	public enum Type
	{
		NONE = 4,
		VERTICAL = 0,
		BILBOARD = 1,
		GROUND = 2
	}

	public event Action onChange;

	[NonSerialized]
	[System.Xml.Serialization.XmlIgnore]
	private static Shader _basicShader;

	[NonSerialized]
	[System.Xml.Serialization.XmlIgnore]
	public Texture2D texture;
	
	[NonSerialized]
	[System.Xml.Serialization.XmlIgnore]
	private Material _material;

	[System.Xml.Serialization.XmlIgnore]
	public float scaleX
	{
		get 
		{
			return ((texture.width > texture.height) ? (texture.width/(float)texture.height) : (1)) * scale; 
		}
	}

	[System.Xml.Serialization.XmlIgnore]
	public float scaleY
	{
		get 
		{
			return ((texture.height > texture.width) ? (texture.height/(float)texture.width) : (1)) * scale; 
		}
	}

	#region Properties
	public string name = "";
	public float scale = 1;
	public Type type = Type.GROUND;
	public bool blockCharacter = false;
	public bool blockProjectile = false;
	public bool useTransparency = false;
	public byte[] encodedTexture;
	public float xDesloc = 0;
	public float yDesloc = 0;
	public bool pathable = false;

	public int id;
	#endregion



	public void OnChange()
	{
		if(onChange != null)
			onChange();
	}

	public Tile GetTileForSerialization()
	{
		Tile __toReturn = new Tile();
		__toReturn.name = name;
		__toReturn.scale = scale;
		__toReturn.type = type;
		__toReturn.blockCharacter = blockCharacter;
		__toReturn.blockProjectile = blockProjectile;
		__toReturn.useTransparency = useTransparency;
		__toReturn.id = id;
		__toReturn.encodedTexture = encodedTexture;
		__toReturn.xDesloc = xDesloc;
		__toReturn.yDesloc = yDesloc;
		__toReturn.pathable = pathable;

		return __toReturn;
	}

	private Shader GetShader()
	{
		if (_basicShader == null) 
		{
			_basicShader = Resources.Load("Shaders/Standard") as Shader;
		}

		return _basicShader;
	}

	public Material GetMaterial()
	{
		if(_material == null)
		{
			_material = new Material (GetShader());
			_material.mainTexture = texture;

			if(useTransparency)
			{
				_material.SetInt("_SrcBlend", 5);
				_material.SetInt("_DstBlend", 10);
				_material.SetInt("_ZWrite", 0);
				_material.DisableKeyword("_ALPHATEST_ON");
				_material.EnableKeyword("_ALPHABLEND_ON");
				_material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				_material.renderQueue = 3000;
			}
			else
			{
				_material.SetInt("_SrcBlend", 1);
				_material.SetInt("_DstBlend", 0);
				_material.SetInt("_ZWrite", 1);
				_material.DisableKeyword("_ALPHATEST_ON");
				_material.DisableKeyword("_ALPHABLEND_ON");
				_material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				_material.renderQueue = -1;
			}
		}

		return _material;
	}
}
