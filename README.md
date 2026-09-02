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

- Object Pool

- Event System

- ScriptableObject

- Upgrade Effect System

- Save / Load

