# Crane Game (Unity)

Unity で制作したクレーンゲームのシミュレーション。
物理演算（Rigidbody / HingeJoint）を用いて、アーケード筐体の挙動を再現。

## 構成
- **Carriage**：XZ 平面を移動する台車  
- **Winch**：上下動するウインチ（昇降）  
- **Finger**：HingeJoint を用いた爪（物理的に景品を掴む）

※ Carriage / Winch は Kinematic Rigidbody + MovePosition で制御  
※ Finger は Non-Kinematic Rigidbody + HingeJoint

## 特徴
- 爪の開閉・保持を **物理演算ベース**で実装
- 景品、床の摩擦を **Physic Material** で調整

## 操作方法
- 水平移動：矢印キー
- 上下移動： Q(上) / A(下)
- 決定：ボタン入力で下降 → 掴み → 上昇

## スクリーンショット
<img width="786" height="557" alt="スクリーンショット 2026-01-03 14 01 19" src="https://github.com/user-attachments/assets/1bf13fca-af41-4f38-9df1-eb54b0c4db72" />
<img width="786" height="557" alt="スクリーンショット 2026-01-03 14 02 37" src="https://github.com/user-attachments/assets/d2d9cbab-12a0-48fe-b088-f604d511499c" />

## 今後の展望

今後は実際のクレーンゲームに見られる多様な状況に、より近づけていきたい。

- **橋渡し（景品を押して位置をずらす）挙動の再現**  
  爪で直接掴むだけでなく、景品を押す・転がす・傾けるといった操作が成立するよう、
  景品形状・質量分布・摩擦係数の調整を行う。

- **確率的要素・個体差の導入**  
  同じ操作でも結果が毎回完全に一致しないよう、
  微小な初期姿勢や摩擦のばらつきを導入し、実機特有の不確実性を再現する。

- **プレイヤー視点の改善**  
  カメラ位置・視野角・ガラス越しの見え方を調整し、
  実際の筐体を覗き込んで操作している感覚に近づける。

これらを通じて、単なる「掴むゲーム」ではなく、
現実のクレーンゲームにおける試行錯誤や戦略性を表現できるシミュレーションを目指す。

