<img width="1000" height="560" alt="스크린샷 2026-02-27 194420" src="https://github.com/user-attachments/assets/b85f945e-2657-4e31-a869-fd1075b04a0f" />  

# 크로노팩토리(Chrono Factory)



## Overview

숭실대 게임 개발 동아리 GAMMARU에서 주최한 2026 겨울공모전에 출품한 작품  
기한내로 '탈출'이라는 키워드를 주제로 게임을 제작하여 발표 및 전시를 하였다.

Unity

1인 개발

본 개발 기간: 2개월(25.12~26.02)
리팩토링 및 개선 기간 : 2주(26.08)

자동화/공장 건설 장르의 게임, 한정된 시간 내에 효율적으로 건물을 배치하여 자원을 확보한뒤 최종 건물을 설치해 클리어
 



## Gameplay



Result Scene

<img width="400" height="240" alt="스크린샷 2026-02-27 194649" src="https://github.com/user-attachments/assets/91250295-420c-4140-b974-21a0287bb863" />

Upgrade Scene

<img width="400" height="240" alt="스크린샷 2026-02-27 194828" src="https://github.com/user-attachments/assets/cbaf53c3-fe63-4b55-9e72-e70c50044ffe" />


게임 플레이 흐름

건물을 설치해 자원을 얻는다, 건물은 자원을 소모하며 주기적으로 위험요소(공해, 열기)를 생성.  
->  
제한 시간이 되거나 위험요소가 일정 수치 이상이 되면 게임오버.  
->  
게임오버 후 플레이 중 얻은 자원을 포인트로 환산(Result Scene).  
->  
포인트로 업그레이드를 구매(Upgrade Scene)한 후 다음 플레이, 제한시간, 위험요소, 자원은 모두 초기화  
->  
최종건물을 설치해 게임을 클리어 할 때 까지 반복  

## Technical Features



- Grid-based Building System

게임 내 건물의 위치와 점유 셀을 GridDataManager를 통해 관리  
[Assets/Project/Scripts/Core/Managers/GridDataManager.cs]  
Vector3Int를 Key로 사용하는 Dictionary를 통해 특정 셀의 건물을 빠르게 조회, 설치 및 제거  
건물의 크기에 따라 여러 셀을 점유 가능, 이 경우 Dictionary내에서 서로 다른 Key가 같은 Value를 가지도록 설정

- Neighbor Building System

건물 간 인접 여부에 따라 생산량 등의 보너스를 적용
건물 생성/제거 시 영향을 받는 인접 타일의 건물을 GridDataManager를 통해 접근하여 인접보너스를 갱신

- Upgrade Effect System

UpgradeData를 통해 기본적인 데이터를 관리, 실제 적용은 UpgradeManager가 가지고 있는 EffectRegistry에 등록된 Effect클래스를 통해 적용  
[Assets/Project/Scripts/Core/Managers/UpgradeManager.cs]  
[Assets/Project/Scripts/Data/UpgradeData/UpgradeData.cs]  
[Assets/Project/Scripts/Data/UpgradeData/UpgradeEffectRegistry.cs]

- Save / Load

json파일을 통해 save/load를 관리

- Object Pool

반복적으로 생성/제거가 일어나는 오브젝트에 대해 Object Pool적용

- ScriptableObject

건물, 자원, 업그레이드 요소의 데이터를 SO로 관리

## Technical Challanges

### 1. 인접 건물 탐색 최적화
 Before
 
 개발 기한을 맞추기 위하여 건물이 생성/삭제 될때마다 씬 내의 모든 건물에 인접 보너스 재계산 명령을 호출하는 단순한 구조

 Problem

 건물 수가 증가할 경우 불필요한 탐색이 과도하게 많아질 가능성 존재

 After

 건물 생성/삭제 시 GridDataManager에서 인접 타일만을 조회하여 실제로 영향을 받는 건물만 재계산하도록 변경

### 2. Upgrade System

 Before
 
 건물마다 해당 업그레이드의 인접 보너스 적용과 업그레이드 Effect를 직접 입력 

 Problem

 업그레이드 수가 많아질 경우 한 건물 스크립트에 효과 처리 로직이 몰릴 가능성 존재

 After

 EffectRegistry와 Effect클래스를 따로 만들어 인접보너스 계산과 효과 적용을 분리, 건물은 EffectType을 통해 Effect에 접근하여 Apply함수를 통해 실제 효과 적용

### 3. Save/Load

 게임 상태를 ID 기반으로 직렬화해 JSON으로 저장,  
 Load시 ID기반으로 Database에 접근하여 실제 Data를 복원

## Develope Progress

### Early
<img width="400" height="240" alt="스크린샷 2026-01-23 190648" src="https://github.com/user-attachments/assets/ffd9758c-3551-4c10-b59a-06f76f5962b0" />  

### Mid
<img width="400" height="240" alt="스크린샷 2026-02-23 150639" src="https://github.com/user-attachments/assets/2a115a73-5a34-4f0a-aa66-082229fe8fd7" />  

### Final
<img width="400" height="240" alt="스크린샷 2026-02-27 194420" src="https://github.com/user-attachments/assets/aaca0c61-5a2d-40f9-8fa0-cf05f6cf22f9" />

## Retrospective

이번 프로젝트에서는 제한된 개발 기간 내에 핵심 게임플레이를 완성한 후,
실제 개발 과정에서 발생한 문제를 분석하고 구조를 개선하는 경험을 하였다.

### What I Learned

**1. 확장성을 고려한 책임 분리**

초기에는 업그레이드 효과를 각 Building에서 직접 처리했지만,
업그레이드가 증가하면서 Building 클래스의 책임이 증가했다.
이를 Effect 클래스로 분리하여 새로운 효과를 추가할 때 기존 Building 코드를
수정하는 범위를 줄였다.

**2. 측정을 기반으로 한 최적화**

인접 건물 탐색 과정에서 불필요한 전체 탐색이 발생하는 것을 확인하고,
GridDataManager의 Dictionary를 활용하여 주변 셀만 조회하도록 개선했다.

이외에도 Update문의 호출을 줄이기 위해 Tickmanager를 활용한 주기 관리를 시도하였으나,
Profiler를 통해 다른 시스템의 CPU 사용량을 확인한 결과
현재 게임 규모에서는 유의미한 병목이 없음을 확인했고,
불필요한 최적화는 진행하지 않았다.

**3. 데이터와 런타임 객체의 분리**

Save/Load를 구현하면서 게임 객체 자체가 아닌 ID와 위치 등의 필요한 상태만
저장하고 Database를 통해 원본 데이터를 복원하는 구조를 적용했다.

**4. 적절한 시점의 추상화**

처음부터 모든 확장 가능성을 고려하여 범용적인 구조를 설계하는 데 지나치게 많은 시간을 투자하기보다는, 
현재 요구사항을 충족할 수 있는 적절한 수준으로 구조를 설계하고 핵심 기능을 먼저 완성하는 것이 제한된 개발 기간에서는 효율적이었다.

개발 초기에 확장성을 고려하여 범용적인 구조를 설계하는데 지나치게 시간을 투자해 개발 진행이 더뎌지자, 
최소한의 구조만 갖추고 기능을 추가하며 개발을 이어갔다.

그러나 오히려 공통적인 패턴과 구조적 문제가 명확해졌기 때문에 더 빠르게 구조 설계가 가능했다. 
이를 통해 모든 가능성을 사전에 추상화하기보다는 실제 요구사항과 개발 방향이 구체화되는 시점에 적절하게 추상화하는 것이 
개발 속도와 유지보수성 사이의 균형을 맞추는 데 효과적이라는 것을 경험했다.



 

 



