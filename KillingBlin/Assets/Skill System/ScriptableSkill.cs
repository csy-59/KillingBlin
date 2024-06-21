using System; // 기본적인 시스템 기능을 사용하기 위해 포함
using System.Collections; // 컬렉션 관련 기능을 사용하기 위해 포함
using System.Collections.Generic; // 제네릭 컬렉션을 사용하기 위해 포함
using System.Text; // 문자열 관련 기능을 사용하기 위해 포함
using UnityEngine; // Unity 엔진 기능을 사용하기 위해 포함

// CreateAssetMenu는 ScriptableObject를 생성할 때 메뉴에 표시될 정보를 설정하는 속성
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/New Skill", order = 0)]

public class ScriptableSkill : ScriptableObject
{
    // 스킬의 업그레이드 데이터를 저장하는 리스트
    public List<UpgradeData> UpgradeData = new List<UpgradeData>();
    public bool IsAbility; // 스킬이 능력인지 여부를 나타내는 플래그
    public string SkillName; // 스킬 이름
    public bool OverwriteDescription; // 설명을 덮어쓸지 여부를 나타내는 플래그
    [TextArea(1, 4)] public string SkillDescription; // 스킬 설명
    public Sprite SkillIcon; // 스킬 아이콘
    public List<ScriptableSkill> SkillPrerequisites = new List<ScriptableSkill>(); // 스킬 전제 조건 목록
    public int SkillTier; // 스킬 티어
    public int Cost; // 스킬 비용

    // 유니티 에디터에서 값이 변경될 때 호출되는 메서드
    private void OnValidate()
    {
        SkillName = name; // 스킬 이름을 오브젝트 이름으로 설정
        if (UpgradeData.Count == 0)
            return; // 업그레이드 데이터가 없으면 반환
        if (OverwriteDescription)
            return; // 설명을 덮어쓰기로 설정한 경우 반환

        // 설명을 생성하는 메서드 호출
        GenerateDescription();
    }

    // 스킬 설명을 생성하는 메서드
    private void GenerateDescription()
    {
        if (IsAbility) // 스킬이 능력인 경우
        {
            switch (UpgradeData[0].StatType) // 첫 번째 업그레이드 데이터의 스탯 타입에 따라 설명 설정
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
        else // 스킬이 능력이 아닌 경우
        {
            StringBuilder sb = new StringBuilder(); // 설명을 생성할 StringBuilder 인스턴스 생성
            sb.Append($"{SkillName} increases "); // 설명 시작 부분 추가
            for (int i = 0; i < UpgradeData.Count; i++) // 업그레이드 데이터를 순회하며 설명 생성
            {
                sb.Append(UpgradeData[i].StatType.ToString()); // 스탯 타입 추가
                sb.Append("  by  "); // " by " 추가
                sb.Append(UpgradeData[i].SkillIncreaseAmount.ToString()); // 증가량 추가
                sb.Append(UpgradeData[i].IsPercentage ? "%" : " points(s)"); // 증가량 단위 추가
                if (i == UpgradeData.Count - 2) // 마지막 두 개 항목 사이에는 " and " 추가
                    sb.Append(" and ");
                sb.Append(i < UpgradeData.Count - 1 ? ", " : "."); // 각 항목 사이에 ", " 추가, 마지막에는 "." 추가
            }

            SkillDescription += sb.ToString(); // 생성된 설명을 SkillDescription에 할당
        }
    }
}

// 업그레이드 데이터를 나타내는 클래스
[System.Serializable]

public class UpgradeData
{
    public StatTypes StatType; // 스탯 타입
    public int SkillIncreaseAmount; // 스킬 증가량
    public bool IsPercentage; // 증가량이 퍼센트인지 여부
}

// 스탯 타입을 정의하는 열거형
public enum StatTypes
{
    Strength, // 힘
    Dexterity, // 민첩성
    Intelligence, // 지능
    Wisdom, // 지혜
    Charisma, // 매력
    Constitution, // 체력
    DoubleJump, // 이중 점프
    Dash, // 대시
    Teleport // 텔레포트
}
