using System.Collections; // 컬렉션 관련 기능을 사용하기 위해 포함
using System.Collections.Generic; // 제네릭 컬렉션을 사용하기 위해 포함
using System.Linq; // LINQ 기능을 사용하기 위해 포함
using UnityEngine; // Unity 엔진 기능을 사용하기 위해 포함

// CreateAssetMenu는 ScriptableObject를 생성할 때 메뉴에 표시될 정보를 설정하는 속성
[CreateAssetMenu(fileName = "New Skill Library", menuName = "Skill System/New Skill Library", order = 0)]
public class ScriptableSkillLibrary : ScriptableObject
{
    // 스킬 라이브러리를 저장하는 리스트
    public List<ScriptableSkill> SkillLibrary;

    // 특정 티어의 스킬 목록을 반환하는 메서드
    public List<ScriptableSkill> GetSkillsOfTier(int tier)
    {
        // LINQ를 사용하여 스킬 라이브러리에서 지정된 티어의 스킬을 필터링하고 리스트로 반환
        return SkillLibrary.Where(skill => skill.SkillTier == tier).ToList();
    }
}

