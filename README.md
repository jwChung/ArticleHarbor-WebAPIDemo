[![Stories in Ready](https://badge.waffle.io/jwChung/ArticleHarbor-WebAPIDemo.png?label=ready&title=Ready)](https://waffle.io/jwChung/ArticleHarbor-WebAPIDemo)
ArticleHarbor [![Build status](https://ci.appveyor.com/api/projects/status/k9k4dd8qjsga7kvp?svg=true)](https://ci.appveyor.com/project/jwChung/articleharbor-webapidemo)
===============

PresentationModel ==> DomainModel <== PersistenceModel

주제
-----
Web API에서 흔히 Service라고 하는 클래스들은 좋은 Abstraction을 가졌는가?

    IAService <=== AService ---> ARepository
                      |
    IBService <=== BService ---> BRepository
                      |
    ICService <=== CService ---> CRepository

 - Service는 Repository 단순 대리자 역할
 - C++ Header interfaces: 하나의 implemention class

**===> The best abstration for Web API???** 


Scenarios
---------

 - ERD

**Roles**

  - Administrator
  - Author
  - User
  - Anonymous

**Visitor Pattern**

  - IModel: Element (Visitee)
  - IModelCommand: Visitor
  - EmptyCommand
  - CompositeCommand
  - CompositeModel

**Delete Scenario**

  - DeleteCommand
  - DeleteBookmarksCommand
  - DeleteKeywordsCommand
  - DeleteConfirmableComman

**Acceptance test vs [Structure Inspection](http://blog.ploeh.dk/2013/04/04/structural-inspection/)**

  - 전체가 잘 작동된다는 것을 보장하기 위해서는 먼저 Structure Inspection을 실시한다.
  - 그러나 Structure Inspection 만으로는 Acceptance(sanity)를 보장할 수 없으므로,
    몇몇 주요부분에 Acceptance test를 실시한다.
  - Structure Inspection 대신에 모든 경우를 Acceptance test로 실시하는 것이 좋으나,
    간단한 조건의 변화들이 만드는 모든 조합의 수를 Acceptance test로 커버하기에는
    상당한 비용이 든다.
  - 수동테스트보다 자동테스트로 해결하고,
  - Integration test보다 Unit test(Structure Inspection)로 해결하는 것이 비용면에서 좋다.

**Insert Scenario**

**Update Scenario**

**ArticleCollectorDaemon**

  - CreateFacebookCommand Method(Transformation)

General Purpose
---------------
  - Funtional programming:  
    Pattern matching = Visitor pattern  
    Record type (F#) = IModel
  - [Homoiconicity](http://vimeo.com/68236489)

DomainModel과 PersistenceModel 분리
----------------------------------

 - Direct reference: DIP 위배
 - Repository 패턴, Enitities 공용: ORM 대치 가능
 - Enitities까지 분리: DB normalization/denormalization 가능.


**Enitities까지 분리를 위해서는?**

 - Repository 패턴
 - IPredicate
 - ISqlQuery

Article delete Scenario 예제


Unit of work 패턴
-----------------


Credits
-------
[한글 형태소 분석기(정성태)](http://www.sysnet.pe.kr/2/0/1500)