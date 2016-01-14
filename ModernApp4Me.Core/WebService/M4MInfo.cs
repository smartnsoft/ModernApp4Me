namespace ModernApp4Me.Core.WebService
{
  
  /// <summary>
  /// Gathers a business object and its response time in milliseconds.
  /// </summary>
  /// <author>Ludovic Roland</author>
  /// <since>2016.01.14</since>
  public sealed class M4MInfo<TBusinessObjectType>
  {

    public TBusinessObjectType Value { get; set; }

    public double ResponseTime { get; set; }

  }

}
