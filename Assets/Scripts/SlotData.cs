[System.Serializable]
public class SlotData
{
    // Handled Separately
    public string saveName;
    public bool particlesEnabled;

    // PlayerController
    public int maxJumpCount;
    public float initialJumpVelocity;
    public float risingJumpDuration;
    public float longJumpMultiplier;
    public float[] position;

    public SlotData(PlayerController playerController) {
        maxJumpCount = playerController.maxJumpCount;
        initialJumpVelocity = playerController.initialJumpVelocity;
        risingJumpDuration = playerController.risingJumpDuration;
        longJumpMultiplier = playerController.longJumpMultiplier;

        position = new float[3];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;
        position[2] = playerController.transform.position.z;
    }
}
