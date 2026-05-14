# AGENTS.md

이 문서는 이 Unity 프로젝트에서 AI 코딩 에이전트가 작업할 때 따라야 할 기준입니다. 답변과 작업 기록은 기본적으로 한국어로 작성합니다.

## 프로젝트 개요

- 프로젝트 유형: Unity 6.4 3D 프로젝트
- Unity 에디터 버전: `6000.4.5f1`
- 렌더 파이프라인: Universal Render Pipeline, `com.unity.render-pipelines.universal` `17.4.0`
- 입력 시스템: Unity Input System, `com.unity.inputsystem` `1.19.0`
- 테스트: Unity Test Framework, `com.unity.test-framework` `1.6.0`
- 주요 패키지: AI Navigation, Timeline, uGUI, Visual Scripting

## 작업 원칙

- 기존 프로젝트 구조와 Unity 관례를 우선합니다.
- 런타임 스크립트는 기본적으로 `Assets/` 아래에 배치합니다.
- 에디터 전용 코드는 반드시 `Editor` 폴더 아래에 둡니다.
- Unity가 생성하는 `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `.vs/`, `*.csproj`, `*.sln`, `*.slnx` 파일은 직접 수정하지 않습니다.
- Unity 에셋을 추가, 이동, 삭제할 때는 대응되는 `.meta` 파일도 함께 유지합니다.
- 씬, 프리팹, 머티리얼, 렌더 파이프라인 에셋 같은 Unity 직렬화 파일은 가능하면 Unity 에디터 또는 안정적인 직렬화 흐름으로 변경합니다.
- 수동으로 YAML 에셋을 수정해야 할 때는 `fileID`, `guid`, `m_Script` 참조를 훼손하지 않도록 변경 범위를 최소화합니다.
- 패키지는 `Packages/manifest.json`에 명시합니다. 패키지 버전을 바꿀 때는 호환성과 잠금 파일 변경 여부를 함께 확인합니다.

## 코딩 규칙

- C# 코드는 Unity와 .NET 표준 관례를 따릅니다.
- `MonoBehaviour` 필드는 인스펙터 노출이 필요한 경우 `private` 필드에 `[SerializeField]`를 사용합니다.
- 불필요한 `public` 필드와 전역 상태를 만들지 않습니다.
- `Update()`에 무거운 탐색, 할당, LINQ, `FindObjectOfType`류 호출을 넣지 않습니다.
- 입력 처리는 새 Input System을 우선 사용합니다. 기존 `Input.GetKey` 방식은 간단한 임시 테스트가 아닌 이상 피합니다.
- 물리 이동은 가능하면 `FixedUpdate()`와 `Rigidbody` 흐름에 맞춥니다.
- 카메라, 무기, 플레이어, 적 AI처럼 결합이 커지기 쉬운 기능은 역할 단위로 스크립트를 분리합니다.
- 씬 오브젝트 이름이나 태그 문자열에 강하게 의존하는 코드는 피하고, 필요한 경우 상수나 직렬화 필드로 모읍니다.
- 주석은 복잡한 의도나 Unity 생명주기상 주의점이 있을 때만 짧게 남깁니다.

## 3D 슈팅 게임 구현 기준

- 플레이어 조작, 조준, 발사, 체력, 데미지, 적 스폰, 피격 반응은 서로 독립적으로 테스트 가능한 작은 컴포넌트로 나눕니다.
- 무기 데이터는 하드코딩보다 `ScriptableObject` 사용을 우선 검토합니다.
- 반복 생성되는 탄환, 이펙트, 적은 오브젝트 풀링을 우선 검토합니다.
- 데미지 처리는 `IDamageable` 같은 명확한 계약을 두고, 구체 타입 캐스팅을 남발하지 않습니다.
- URP 환경을 기준으로 머티리얼, 라이트, 포스트 프로세싱, 볼륨 설정을 다룹니다.
- NavMesh 또는 AI Navigation을 사용할 때는 베이크 설정, 에이전트 반경, 장애물 처리 변경을 함께 확인합니다.

## 파일 및 폴더 가이드

- `Assets/Scenes/`: 씬 파일
- `Assets/Settings/`: URP와 프로젝트 렌더링 설정
- `Assets/InputSystem_Actions.inputactions`: Input System 액션 에셋
- `Packages/manifest.json`: Unity 패키지 의존성
- `ProjectSettings/ProjectVersion.txt`: Unity 에디터 버전

새 기능을 추가할 때는 필요에 따라 다음 구조를 사용합니다.

```text
Assets/
  Scripts/
    Player/
    Weapons/
    Enemies/
    Gameplay/
    UI/
  Prefabs/
  Materials/
  ScriptableObjects/
  Tests/
```

## 테스트와 검증

Unity CLI 경로가 환경마다 다를 수 있으므로, 가능하면 `UNITY_EDITOR` 환경 변수에 Unity 실행 파일 경로를 지정해서 사용합니다.

```powershell
& $env:UNITY_EDITOR -batchmode -quit -projectPath . -runTests -testPlatform EditMode -testResults TestResults/EditMode.xml
& $env:UNITY_EDITOR -batchmode -quit -projectPath . -runTests -testPlatform PlayMode -testResults TestResults/PlayMode.xml
```

검증 우선순위는 다음과 같습니다.

1. 변경한 C# 코드가 컴파일되는지 확인합니다.
2. 관련 EditMode 또는 PlayMode 테스트를 실행합니다.
3. 씬, 프리팹, 입력 액션, 렌더 설정을 바꿨다면 Unity 에디터에서 참조 누락과 콘솔 오류를 확인합니다.
4. 플레이어 이동, 조준, 발사, 충돌, 데미지, UI 같은 게임플레이 변경은 실제 Play Mode에서 확인합니다.

## Git 및 변경 관리

- 사용자 변경사항을 되돌리지 않습니다.
- 요청 범위 밖의 리팩터링은 피합니다.
- Unity 에셋 변경 시 `.meta` 파일 포함 여부를 확인합니다.
- 큰 바이너리 에셋, 빌드 산출물, 캐시 파일은 커밋 대상에 포함하지 않습니다.
- Git에서 `dubious ownership` 오류가 나면 사용자 승인 없이 전역 `safe.directory`를 추가하지 않습니다.

## 응답 규칙

- 사용자에게는 한국어로 간결하게 보고합니다.
- 변경한 파일, 검증한 명령, 실행하지 못한 검증이 있으면 명확히 적습니다.
- Unity 에디터 실행이나 네트워크 접근처럼 권한 또는 로컬 환경에 의존하는 작업은 필요한 경우 먼저 알립니다.

## 프로젝트 전용 추가 지침

- Unity 6.4로 만드는 간단한 슈팅 게임 프로젝트다.
- 화면은 2D 슈팅처럼 보이지만, Unity의 3D 공간을 기준으로 만든다.
- 한 번에 많은 기능을 만들지 말고, 요청한 기능만 구현한다.
- 다음 단계에 필요할 것 같다는 이유로 파일, 폴더, 오브젝트를 미리 만들지 않는다.
- 기존 내용은 최대한 유지하고, 필요한 부분만 수정한다.
- 요청 범위를 벗어난 정리, 리팩터링, 구조 변경은 하지 않는다.
- 코드는 초보자도 따라갈 수 있게 단순하게 작성한다.
- 복잡한 구조보다 눈으로 동작을 확인하기 쉬운 구현을 우선한다.
- 변수명과 함수명은 역할을 알 수 있게 작성한다.
- 필요하면 짧은 한글 주석을 사용한다.
- 코드만으로 끝나지 않는 작업은 Unity Editor에서 해야 할 일을 따로 알려준다.
- 새 스크립트를 만들었다면 어느 오브젝트에 붙일지 알려준다.
- Inspector에서 설정할 값이 있다면 함께 알려준다.
- 작업이 끝나면 수정한 파일, 새로 만든 파일, 주요 변경 내용, Unity Editor에서 할 일을 짧게 보고한다.
- 각 단계 프롬프트에 `완료 후 추가 보고 형식`이 있으면 그 내용도 함께 보고한다.
