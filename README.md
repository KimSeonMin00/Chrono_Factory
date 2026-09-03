<img width="1000" height="560" alt="스크린샷 2026-02-27 194420" src="https://github.com/user-attachments/assets/b85f945e-2657-4e31-a869-fd1075b04a0f" />  

# 크로노팩토리(Chrono Factory)



## Overview



Unity

1인 개발

개발 기간: 2개월

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




