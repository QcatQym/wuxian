using KSerialization;
using STRINGS;
using UnityEngine;

namespace wuxian
{
  [SerializationConfig(MemberSerialization.OptIn)]
  public class WirelessSignalReceiverINterFace : KMonoBehaviour, IIntSliderControl
  {
    [field: Serialize]
    public int ReceiveChannel { get; set; }

    [field: Serialize]
    public int Signal = 300;

    [Serialize]
    private int _receiverId;
    [Serialize]
    public float maxCount=10;


    private MeterController meter;

    protected override void OnSpawn()
    {
      _receiverId = WirelessAutomationManager.RegisterReceiver(new SignalReceiver(ReceiveChannel, gameObject));

      Subscribe(WirelessAutomationManager.WirelessLogicEvent, OnWirelessLogicEventChanged);
      KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
      this.meter = new MeterController(component, "meter_target", component.FlipY ? "meter_dn" : "meter_up", Meter.Offset.Infront, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
      this.UpdateVisualState();
    
  }

    protected override void OnCleanUp()
    {
      Unsubscribe((int)GameHashes.OperationalChanged, OnWirelessLogicEventChanged);
      WirelessAutomationManager.UnregisterReceiver(_receiverId);
    }

    private void OnWirelessLogicEventChanged(object data)
    {
      var ev = (WirelessLogicValueChanged)data;

      if (Signal != ev.Signal && ev.Channel == ReceiveChannel)
      {
        ChangeState(ev.Signal);
      }
    }

    private void ChangeState(int signal)
    {
      Signal = signal;
      UpdateVisualState();

    }

    private void ChangeListeningChannel(int channel)
    {
      ReceiveChannel = channel;
      WirelessAutomationManager.ChangeReceiverChannel(_receiverId, ReceiveChannel);
    }
    public void UpdateVisualState()
    {
      float num0 =this.Signal /this.maxCount ;
      int num= (int)(num0 %10);
      this.meter.SetPositionPercent(num / 10f);

    }

    public int SliderDecimalPlaces(int index) => 0;
    public float GetSliderMin(int index) => 0;
    public float GetSliderMax(int index) => 100;
    public float GetSliderValue(int index) => ReceiveChannel;
    public void SetSliderValue(float value, int index) => ChangeListeningChannel(Mathf.RoundToInt(value));
    public string GetSliderTooltipKey(int index) => WirelessAutomationManager.SliderTooltipKey;
    public string GetSliderTooltip() => $"Will listen to signal broadcast on {UI.PRE_KEYWORD}channel {ReceiveChannel}{UI.PST_KEYWORD}";
    public string SliderTitleKey => WirelessAutomationManager.SliderTitleKey;
    public string SliderUnits => string.Empty;
  }
}
