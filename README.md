# MahAPP을 활용한 BUSTOP 관리자 화면 WPF
2022.05.02 제작

## 개발목적
- BUSTOP 앱의 종합적인 관리를 위한 관리자 화면을 만들기 위함

## 기술스택
- C#(WPF)
- MahAPP
- MySQL

1. C# MahApp / MySQL 활용을 위한 누겟 패키지 설치
  - MahApps.Metro
  - MahApps.Metro.IconPacks
  - MySql.Data
2. MySQL DB 추가 및 수정
  - 버스 정보를 담는 table
    - 
  - 관리자 아이디/패스워드를 담는 table
    - 
3. MahApps를 활용한 ui 디자인 및 기능 구현
  1) 메인 화면 
    - 
  2) 로그인 화면 
    - 
  3) 버스 정보 추가 화면
    - 

## 로직
- 미리 지정해둔 관리자의 id/pw 를 이용해 관리자모드 활성화(MySQL Login)
- 관리자모드 비활성화 상태에서 현황 조회 및 정보 추가 불가능(bool 함수로 구분)
- 로그인 이후 현황 조회를 통해 bus_table의 모든 정보를 확인 가능
- 버스 정보 추가를 통해 DB에 데이터 삽입 가능
- 로그아웃 버튼을 통해 관리자 모드 비활성화 

### 실행화면
