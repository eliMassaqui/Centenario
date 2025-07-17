# 📘 GDD — Game Design Document

## 🎮 Título: Centenário: Aventura na Serra da Leba

---

## 🧠 Visão Geral

**Centenário: Aventura na Serra da Leba** é um jogo **runner 3D** ambientado em Angola, inspirado na icônica estrada da **Serra da Leba**. O jogador deve percorrer a maior distância possível, desviando de obstáculos e coletando **máscaras culturais** como forma de preservar a história e cultura do país. Cada progresso é acompanhado de conquistas e feedbacks visuais.

---

## 🌟 Objetivo Principal

* Correr o mais longe possível
* Desviar de obstáculos
* Coletar máscaras culturais
* Desbloquear conquistas e títulos

---

## 🎯 Mecânicas de Jogo

### 🏃️ Controlo do Jogador

* Movimento automático para frente
* Controles laterais (teclas A/D ou setas) para desviar

### 🛁 Obstáculos

* Obstáculos espalhados pelo cenário
* Colisão resulta em fim de jogo

### 🌟 Colecionáveis

* Máscaras com valor simbólico e cultural
* Cada máscara aumenta o contador geral

### 🛍️ Interface (HUD)

* Distância percorrida (em metros)
* Tempo de corrida
* Total de máscaras coletadas
* Recorde (melhor distância)
* Tempo total acumulado
* Jogadas totais
* Feedback a cada 500 metros ("Marco alcançado!")

---

## 🔹 Sistema de Conquistas

### 👽 Máscaras

* Baseado no número total de máscaras coletadas
* 3 conquistas desbloqueáveis:

| Nível | Máscaras Necessárias | Nome do Desbloqueio |
| ----- | -------------------- | ------------------- |
| 1     | 70                   | Colecionador        |
| 2     | 200                  | Explorador          |
| 3     | 1500                 | Mestre das Máscaras |

* Cada conquista desbloqueia um **botão clicável** com imagem ou vídeo informativo sobre a cultura angolana.

### 🏆 Títulos por Distância

* O jogador recebe um **título honorífico** baseado na distância total percorrida.
* Exibe mensagem interativa na tela principal:

> "Você percorreu até X metros. Ganhou o título de Y!"

| Distância (m) | Título Recebido |
| ------------- | --------------- |
| 0             | Novato          |
| 500           | Turista         |
| 1000          | Explorador      |
| 1500          | Viajante        |
| 2000          | Corajoso        |
| 2500          | Intrépido       |
| 3000          | Mestre          |
| 3500          | Veterano        |
| 4000          | Lendário        |
| 4500+         | Ícone           |

---

## 📊 Interface do Menu

* **Estatísticas acumuladas:**

  * Tempo total jogado
  * Recorde de distância
  * Jogadas totais
  * Marcos alcançados
* **Estatísticas da última corrida:**

  * Distância final
  * Tempo final
  * Número de máscaras
* **Botões principais:**

  * Jogar
  * Sair
  * Mostrar/Ocultar Conquistas

---

## 📒 Sistema de Salvamento

* Implementado via `PlayerPrefs`
* Salva:

  * Recorde
  * Tempo Total
  * Jogadas
  * Máscaras Totais
  * Estatísticas da última partida

---

## 🚀 Tecnologias Utilizadas

* **Unity 2022.3.6f1**
* **C#** (programação dos scripts)
* **TextMeshPro** (UI)
* **Line Renderer** (efeitos visuais da serra)

---

## 🌍 Contexto Cultural

O jogo valoriza a cultura angolana através das **máscaras tradicionais**, **nomes simbólicos** e a **Serra da Leba** como elemento cenográfico. Cada conquista oferece um momento de celebração, aprendizado e reconhecimento cultural.

---

## ✅ Estado Atual

* Sistema de conquistas por máscaras: concluído
* Títulos por distância: concluído
* HUD dinâmica com feedback: funcional
* Design visual das curvas da serra: implementado

---

## 📔 Futuras Expansões

* Mais conquistas culturais com vídeos e imagens
* Integração com leaderboard global
* Sistema de customização de personagem
* Modo educacional com curiosidades sobre cada máscara
