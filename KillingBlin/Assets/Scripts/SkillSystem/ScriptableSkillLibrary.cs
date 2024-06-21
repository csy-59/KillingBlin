using System.Collections; // �÷��� ���� ����� ����ϱ� ���� ����
using System.Collections.Generic; // ���׸� �÷����� ����ϱ� ���� ����
using System.Linq; // LINQ ����� ����ϱ� ���� ����
using UnityEngine; // Unity ���� ����� ����ϱ� ���� ����

// CreateAssetMenu�� ScriptableObject�� ������ �� �޴��� ǥ�õ� ������ �����ϴ� �Ӽ�
[CreateAssetMenu(fileName = "New Skill Library", menuName = "Skill System/New Skill Library", order = 0)]
public class ScriptableSkillLibrary : ScriptableObject
{
    // ��ų ���̺귯���� �����ϴ� ����Ʈ
    public List<ScriptableSkill> SkillLibrary;

    // Ư�� Ƽ���� ��ų ����� ��ȯ�ϴ� �޼���
    public List<ScriptableSkill> GetSkillsOfTier(int tier)
    {
        // LINQ�� ����Ͽ� ��ų ���̺귯������ ������ Ƽ���� ��ų�� ���͸��ϰ� ����Ʈ�� ��ȯ
        return SkillLibrary.Where(skill => skill.SkillTier == tier).ToList();
    }
}

