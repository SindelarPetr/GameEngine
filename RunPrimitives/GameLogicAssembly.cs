using System;

namespace GameEngine.RunPrimitives
{
	/// <summary>
	/// This attribute must be used on the assembly where all screen types are contained
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	public class GameLogicAssembly : Attribute
	{

	}
}
