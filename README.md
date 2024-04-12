# <span lang="zh_cn">螺母战记</span> / <span lang="zh_tw">螺母戰記</span> / <span lang="en">EESaga</span> / <span lang="ja">螺母戦記</span>

<p align="center">
  <img src="EESaga.svg" height="128">
</p>

<p align="center">
  <a href="https://github.com/open531/EESaga/graphs/contributors" alt="Contributors">
    <img src="https://img.shields.io/github/contributors/open531/EESaga" /></a>
  <a href="https://github.com/open531/EESaga/pulse" alt="Activity">
    <img src="https://img.shields.io/github/commit-activity/m/open531/EESaga" /></a>
  <a href="https://circleci.com/gh/open531/EESaga/tree/master">
    <img src="https://img.shields.io/circleci/project/github/open531/EESaga/master" alt="build status"></a>
</p>

<p align="center">
电子系第七届软设参赛作品
</p>

## 构建方式

1. 安装依赖项；
    - [.NET SDK 8.0](https://dotnet.microsoft.com/download)
2. 克隆本仓库；
    ```bash
    git clone https://github.com/open531/EESaga.git
    ```
3. 使用 [Godot Engine - .NET 4.2.1](https://godotengine.org/download/) 打开 `project.godot`；
4. 点击 `Project` -> `Export`；
5. 选择对应的平台和输出路径。

## 设计说明书

**<span lang="zh_cn">《螺母战记》</span>** / **<span lang="zh_tw">《螺母戰記》</span>** / ***<span lang="en">EESaga</span>*** / **<span lang="ja">《螺母戦記》</span>** 是一款由 [@open531](https://github.com/open531) 自主研发的箱庭世界策略卡牌战棋游戏。游戏发生在一个被称作「螺母楼」的幻想世界，在这里，被十门核心课选中的人将被授予「万花尺」，导引卡牌之力。你将扮演一位名为「大学生」的神秘角色，在螺母楼的攀登中邂逅性格各异、能力独特的同伴们，和他们一起击败强敌，找回失散的绩点——同时，逐步发掘「鱼骨图」的真相。

### 作品名称

螺母战记 / EESaga

### 创意来源

本软件灵感源自于《杀戮尖塔》、《火焰之纹章》系列、《女神异闻录》系列等游戏。本作品结合了卡牌游戏、战棋游戏、回合制游戏等元素，玩家可以在游戏中体验到不同与其他游戏的战斗策略。

### 主要功能

本软件虚构了一个一名大学生勇闯螺母楼、拯救校园的故事。主角所在的校园发生了异变，螺母楼结构发生了变化，而且校园中出现了大量的敌人。主角需要攀登螺母楼，解开螺母楼的秘密，拯救校园。

螺母楼共有 101 层。其中，前 100 层为战斗关卡，第 101 层为 BOSS 关卡。每层关卡中，主角需要击败所有敌人，才能进入下一层。在每层关卡中，主角可以使用卡牌进行战斗。

战斗关卡共有 10 个不同的主题。每 10 层为一个主题，主题分别是：程序设计、电子电路、数据算法、数字逻辑、概率随机、信号系统、电磁场波、通信网络、媒体认知、固体物理。每个主题有特色的敌人，分别是：蟒蛇、电容、二叉树、芯片、薛定谔猫、傅立叶、麦克斯韦、香龙、感知鸡、单晶龟。

BOSS 关卡中，将会遇到最终 BOSS：「鱼骨守护者」。鱼骨守护者是鱼骨图的守护者，它拥有强大的力量，是主角的最终对手。击败鱼骨守护者后，主角将获得「鱼骨图」，解开螺母楼的秘密，找回失散的绩点，拯救校园，游戏结束。

卡牌系统和战棋系统是本软件的核心功能。

每完成一个楼层，玩家可以抽取一张卡牌，加入到自己的卡组中。卡牌分为攻击、防御、技能、物品四大类。每当轮到玩家的回合，游戏将从玩家的「牌堆」中抽取一定数量的卡牌，添加到玩家的「手牌」中。玩家可以使用手牌中的卡牌，进行攻击、防御、使用技能等操作。使用卡牌需要消耗「能量」。使用完的卡牌将会被放入「弃牌」，直到玩家的「牌堆」中没有卡牌时，将会从「弃牌」中洗牌，重新组成「牌堆」。

每进入一个新的楼层，玩家和队友、敌人将会完全随机地出现在地图上。玩家可以使用鼠标点击地图上的位置，移动自己和队友。玩家可以点击卡牌，对敌人进行攻击、防御、使用技能等操作。玩家需要击败所有敌人，才能进入下一层。

本软件有自动保存功能，玩家每完成一个楼层，都可以自动保存游戏进度。此外，玩家可以在游戏中随时保存游戏进度，下次进入游戏时，可以继续上次的游戏进度。

### 所用技术

- 本软件使用 C# 语言编写。
- 本软件使用 Godot 引擎进行开发。

### 分工情况

- 企划：[@Panxuc](https://github.com/Panxuc)
- 程序：[@DuGangfeng](https://github.com/DuGangfeng) [@Panxuc](https://github.com/Panxuc)
- 美术：[@j-huang22](https://github.com/j-huang22) [@Panxuc](https://github.com/Panxuc)
- 关卡设计：[@DuGangfeng](https://github.com/DuGangfeng)
- 人物设计：[@j-huang22](https://github.com/j-huang22)
- 卡牌设计：[@j-huang22](https://github.com/j-huang22)

## 使用说明

本软件为单机游戏，无需联网。在游戏中，玩家可以使用鼠标进行操作。

- 在玩家的回合中，在地图上点击可以移动。
- 玩家可以点击卡牌进行攻击、防御、使用技能等。