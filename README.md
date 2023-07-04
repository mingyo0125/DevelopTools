# 🧰 DevelopTools
 - 게임 개발을 할떄 대부분 프로젝트가 필요하지만 매번 추가하기 귀찮은 것들을 모아두었습니다.
---

 ### 🗑️ PoolManager :
 - 오브젝트를 제네릭 풀링 할 수 있는 도구
 - 사용방법 :
 1.  게임매니저를 생성 한 뒤 게임 매니저에 오브젝트를 담은 SO를 넣는다.
 2.  생성을 실행할 스크립트에서 PoolAbleMono를 상속 받은 뒤 Init메서드를 작성한다
 3.  생성 하고 싶은 곳에서 타입캐스트를 한 PoolManager.Instance.Pop(오브젝트);를 하고
 4.  제거 하고 싶은 곳에서 PoolManager.Instance.Push(오브젝트);를 한다.

 ## 🏃🏻 Basic3DMovement :
 - 3D에서 기본적인 움직임(이동, 점프, 대쉬)를 이용할 수 있는 도구
 - 사용방법 :
 1.  이 스크립트를 게임오브젝트에 추가 한다.
 2.  Inspector창에서 필요한 값들을 입력한다.

 ## 🎨 ColorInHierarchy :
 - Hierarchy 창에 있는 오브젝트를 커스터마이징 할 수 있는 도구
 - 사용방법 : 이 스크립트를 게임오브젝트에 추가 한 후 Hierarch창에서
 1. prfix에 오브젝트 이름 앞에 올 기호
 2. backColor에 배경 색,
 3. FontColor에 글자 폰트 색을 지정해준다.
 
 ### 🖼️ WebRequest :
 - 인터넷에 있는 텍스쳐를 유니티에서 사용할 수 있는 도구
 - 사용방법 :
 1. 이 스크립트를 게임오브젝트에 추가 한다.
 2. Hierarch창에서 url에 사용할 이미지의 url 주소
 3. pixelPerUnit에 텍스쳐의 pixelPerUnit값
 4. pivot에 텍스쳐를 그릴 pivot을 지정해준다.
          
---
