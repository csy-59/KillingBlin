using System; // �⺻���� �ý��� ����� ����ϱ� ���� ����
using System.Collections; // �÷��� ���� ����� ����ϱ� ���� ����
using System.Collections.Generic; // ���׸� �÷����� ����ϱ� ���� ����
using System.Text; // ���ڿ� ���� ����� ����ϱ� ���� ����
using UnityEngine; // Unity ���� ����� ����ϱ� ���� ����

// CreateAssetMenu�� ScriptableObject�� ������ �� �޴��� ǥ�õ� ������ �����ϴ� �Ӽ�
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/New Skill", order = 0)]

public class ScriptableSkill : ScriptableObject
{
    // ��ų�� ���׷��̵� �����͸� �����ϴ� ����Ʈ
    public List<UpgradeData> UpgradeData = new List<UpgradeData>();
    public bool IsAbility; // ��ų�� �ɷ����� ���θ� ��Ÿ���� �÷���
    public string SkillName; // ��ų �̸�
    public bool OverwriteDescription; // ������ ����� ���θ� ��Ÿ���� �÷���
    [TextArea(1, 4)] public string SkillDescription; // ��ų ����
    public Sprite SkillIcon; // ��ų ������
    public List<ScriptableSkill> SkillPrerequisites = new List<ScriptableSkill>(); // ��ų ���� ���� ���
    public int SkillTier; // ��ų Ƽ��
    public int Cost; // ��ų ���

    // ����Ƽ �����Ϳ��� ���� ����� �� ȣ��Ǵ� �޼���
    private void OnValidate()
    {
        SkillName = name; // ��ų �̸��� ������Ʈ �̸����� ����
        if (UpgradeData.Count == 0)
            return; // ���׷��̵� �����Ͱ� ������ ��ȯ
        if (OverwriteDescription)
            return; // ������ ������ ������ ��� ��ȯ

        // ������ �����ϴ� �޼��� ȣ��
        GenerateDescription();
    }

    // ��ų ������ �����ϴ� �޼���
    private void GenerateDescription()
    {
        if (IsAbility) // ��ų�� �ɷ��� ���
        {
            switch (UpgradeData[0].StatType) // ù ��° ���׷��̵� �������� ���� Ÿ�Կ� ���� ���� ����
            {
                case StatTypes.DoubleJump:
                    SkillDescription = $"{SkillName} grants the Double Jump ability.";
                    break;
                case StatTypes.Dash:
                    SkillDescription = $"{SkillName} grants the Dash ability.";
                    break;
                case StatTypes.Teleport:
                    SkillDescription = $"{SkillName} grants the Teleport ability.";
                    break;
            }
        }
        //throw new NotImplementedException();
        else // ��ų�� �ɷ��� �ƴ� ���
        {
            StringBuilder sb = new StringBuilder(); // ������ ������ StringBuilder �ν��Ͻ� ����
            sb.Append($"{SkillName} increases "); // ���� ���� �κ� �߰�
            for (int i = 0; i < UpgradeData.Count; i++) // ���׷��̵� �����͸� ��ȸ�ϸ� ���� ����
            {
                sb.Append(UpgradeData[i].StatType.ToString()); // ���� Ÿ�� �߰�
                sb.Append("  by  "); // " by " �߰�
                sb.Append(UpgradeData[i].SkillIncreaseAmount.ToString()); // ������ �߰�
                sb.Append(UpgradeData[i].IsPercentage ? "%" : " points(s)"); // ������ ���� �߰�
                if (i == UpgradeData.Count - 2) // ������ �� �� �׸� ���̿��� " and " �߰�
                    sb.Append(" and ");
                sb.Append(i < UpgradeData.Count - 1 ? ", " : "."); // �� �׸� ���̿� ", " �߰�, ���������� "." �߰�
            }

            SkillDescription += sb.ToString(); // ������ ������ SkillDescription�� �Ҵ�
        }
    }
}

// ���׷��̵� �����͸� ��Ÿ���� Ŭ����
[System.Serializable]

public class UpgradeData
{
    public StatTypes StatType; // ���� Ÿ��
    public int SkillIncreaseAmount; // ��ų ������
    public bool IsPercentage; // �������� �ۼ�Ʈ���� ����
}

// ���� Ÿ���� �����ϴ� ������
public enum StatTypes
{
    Strength, // ��
    Dexterity, // ��ø��
    Intelligence, // ����
    Wisdom, // ����
    Charisma, // �ŷ�
    Constitution, // ü��
    DoubleJump, // ���� ����
    Dash, // ���
    Teleport // �ڷ���Ʈ
}
