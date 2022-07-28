using KSerialization;
using STRINGS;
using UnityEngine;

namespace wuxian
{
  [SerializationConfig(MemberSerialization.OptIn)]
  public class WirelessSignalPresureEmitter : KMonoBehaviour, IIntSliderControl, ISim1000ms
  {
    [field: Serialize]
    public int EmitChannel { get; set; }

    [Serialize]
    private int _emitterId;


    protected override void OnSpawn()
    {
      _emitterId = WirelessAutomationManager.RegisterEmitter(new SignalEmitter(EmitChannel, 300));
    }

    protected override void OnCleanUp()
    {
      Unsubscribe((int)GameHashes.OperationalChanged, OnLogicEventChanged);
      WirelessAutomationManager.UnregisterEmitter(_emitterId);
    }

    private void OnLogicEventChanged(object data)
    {
      var signal = ((WirelessLogicValueChanged)data).Signal;
      WirelessAutomationManager.SetEmitterSignal(_emitterId, signal);
    }
    public void Sim1000ms(float dt)
    {
      int i = Grid.PosToCell(this);
      if (Grid.Mass[i] > 0f)
      {
        WirelessLogicValueChanged wirelessLogicValueChanged = new WirelessLogicValueChanged
        {
          Channel = this.EmitChannel,
          Signal = (int)Grid.Mass[i]
        };
        this.OnLogicEventChanged(wirelessLogicValueChanged);
      }
    }
    private void ChangeEmitChannel(int channel)
    {
      EmitChannel = channel;
      WirelessAutomationManager.ChangeEmitterChannel(_emitterId, EmitChannel);
    }
    public int _signal = 10;
    public int SliderDecimalPlaces(int index) => 0;
    public float GetSliderMin(int index) => 0;
    public float GetSliderMax(int index) => 100;
    public float GetSliderValue(int index) => EmitChannel;
    public void SetSliderValue(float value, int index) => ChangeEmitChannel(Mathf.RoundToInt(value));
    public string GetSliderTooltipKey(int index) => WirelessAutomationManager.SliderTooltipKey;
    public string GetSliderTooltip() => $"Will broadcast received signal on {UI.FormatAsKeyWord($"channel {EmitChannel}")}";
    public string SliderTitleKey => WirelessAutomationManager.SliderTitleKey;
    public string SliderUnits => string.Empty;
  }
}
