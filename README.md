ArticleHarbor [![Build status](https://ci.appveyor.com/api/projects/status/k9k4dd8qjsga7kvp?svg=true)](https://ci.appveyor.com/project/jwChung/articleharbor-webapidemo)
===============

ASP.NET Web API 2 Demo project.

기술적인 내용
------------

- 테스트하는 프로젝트와 테스트하지 않는 프로젝트를 구분하여, 프로젝트를 구성하고,
  테스트하지 않는 프로젝트는 테스트하지 않아도 될 만큼 간단히 구성한다.([Humble Object Pattern]
  (http://xunitpatterns.com/Humble%20Object.html))
  - 테스트 O:
	- WebApiPresentationModel
    - DomainModel
	- EFPersistenceModel

  - 테스트 X:
    - Website
	- EFDataAccess

- DomainModel에서는 다른 계층과 완전히 분리되어야 하며, DomainModel에서는
  어떠한 프로젝트를 참조하지 않는다. 달리 말해, 주요하지 않는 계층의 수정으로 도메인이
  수정되는 일은 없어야한다.  
  DomainModel에서 PersistenceModel이 완전히 분리됨으로, 차후 EF가 아닌 다른 데이터 계층을 사용하는 것을
  고려할 수도 있다.

- Xml포멧을 다음과 같은 이유로 명시적으로 지원하지 않는다. (XmlMediaTypeFormatter제거) 
  - Jason이 더 용량도 작고, 자바스크립트에서 바로 사용할 수 있는 형태란 장점을 지닌다.
  - Xml을 사용하는 경우(클라이언트)가 명확하지 않는 시점에서 Xml 지원을 위해 테스트하고
    시간들 들여 개발하는 것은 낭비다. 차후 Xml을 사용해야할 경우가 생기면 그때 개발하도록 한다.



Credits
-------
[한글 형태소 분석기(정성태)](http://www.sysnet.pe.kr/2/0/1500)