# MahAPP을 활용한 BUSTOP 관리자 화면 WPF
2022.05.02 제작

## 개발목적
- BUSTOP 앱의 종합적인 관리를 위한 관리자 화면을 만들기 위함

## 기술스택
- C#(WPF)
- MahAPP
- MySQL

### 1. C# MahApp / MySQL 활용을 위한 누겟 패키지 설치
  - MahApps.Metro
  - MahApps.Metro.IconPacks
  - MySql.Data
### 2. MySQL DB 추가 및 수정
  - #### 버스 정보를 담는 table
    - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/MySQL_Bus_Table.png"/>
  - #### 관리자 아이디/패스워드를 담는 table
    - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/MySQL_Manager_Table.png"/>
### 3. MahApps를 활용한 ui 디자인 및 기능 구현
  - #### 메인 화면 
    - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/MainWindow.png"/>
  - #### 로그인 화면 
    - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/LoginWindow.png"/>
  - #### 버스 정보 추가 화면
    - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/insertBusInfo.png"/>

## 로직
- 미리 지정해둔 관리자의 id/pw 를 이용해 관리자모드 활성화(MySQL Login)
- 관리자모드 비활성화 상태에서 현황 조회 및 정보 추가 불가능(bool 함수로 구분)
- 로그인 이후 현황 조회를 통해 bus_table의 모든 정보를 확인 가능
- 버스 정보 추가를 통해 DB에 데이터 삽입 가능
- 로그아웃 버튼을 통해 관리자 모드 비활성화 

## 실행화면
- <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/AdminPage_Execute.gif" width=700 />

## 05.01 소스코드 수정
- MainWindow 버스 번호 출력 형태 수정(100->100번)
  - <img src="https://raw.githubusercontent.com/PKNU-IOT3/bustop_adminpage/main/images/0501_ModifyMainWindow.png"/>

## 05.02 소스코드 수정
- insertbusinfo.xaml.cs 수정
  - DB에 이미 존재하는 버스 번호인지 확인하여 중복인 경우 DB Insert 제한
  - 저장과 동시에 DB 조회 내용을 다시 뿌려주기
    - MainWindow.xaml.cs 의 BtnBusInfor_Click 메서드를 public으로 선언
    - insertbusinfo.xaml.cs 에서 import(using) 후 버스정보 추가와 동시에 MainWindow 객체를 생성,조회 메서드 호출하여 뿌림
