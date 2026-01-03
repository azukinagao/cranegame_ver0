# Crane Game (Unity)

Unity で制作したクレーンゲームのシミュレーションです。  
物理演算（Rigidbody / HingeJoint）を用いて、アーケード筐体の挙動を再現しています。

## 🎮 概要
- 開発環境：Unity（2022 以降想定）
- 言語：C#
- ジャンル：クレーンゲーム（UFOキャッチャー）

## 🧱 構成
- **Carriage**：XZ 平面を移動する台車  
- **Winch**：上下動するウインチ（昇降）  
- **Finger**：HingeJoint を用いた爪（物理的に景品を掴む）

※ Carriage / Winch は Kinematic Rigidbody + MovePosition で制御  
※ Finger は Non-Kinematic Rigidbody + HingeJoint

## ✨ 工夫した点
- 爪の開閉・把持を **物理演算ベース**で実装
- 景品の滑りやすさを **Physic Material** で調整
- 操作性と安定性のバランス調整（Joint / Mass / Drag）

## ▶ 操作方法
- 移動：キーボード or ゲームパッド（Input System）
- 決定：ボタン入力で下降 → 掴み → 上昇

## 📸 スクリーンショット
<img width="786" height="557" alt="スクリーンショット 2026-01-03 14 01 19" src="https://github.com/user-attachments/assets/1bf13fca-af41-4f38-9df1-eb54b0c4db72" />
<img width="786" height="557" alt="スクリーンショット 2026-01-03 14 02 37" src="https://github.com/user-attachments/assets/d2d9cbab-12a0-48fe-b088-f604d511499c" />

## 📂 リポジトリ構成
- `Assets/`：スクリプト・シーン・マテリアル
- `Packages/`：Unity Package Manager 設定
- `ProjectSettings/`：プロジェクト設定
