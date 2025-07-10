---
name: Feature Addition
about: Add new feature to the project.
title: ''
labels: ''
assignees: ''

---

### 🆕 Feature Summary
**Feature Name**: [e.g. Player Movement System]  
**Goal**: Briefly explain what this feature should do and why it's needed.

---

### 🎯 Description
Describe what the feature does in detail. Mention how it connects to the current gameplay, and what the player will experience.

Example:
> The player should be able to move using WASD or arrow keys, including 8-directional movement. The movement should feel smooth and responsive, using `move_and_slide()`.

---

### 📂 Tasks Breakdown
- [ ] Create new scene: `Player.tscn`
- [ ] Add `Player.gd` script and attach it
- [ ] Implement movement using input from `InputManager.gd`
- [ ] Set up animations for walk cycles
- [ ] Test movement on sample level

---

### 🔁 Dependencies / Related
- Relies on `InputManager.gd` being in place
- Ties into camera tracking system
- Related: #12 (Level Loader)

---

### ✅ Acceptance Criteria
- Player moves in 8 directions
- Movement speed is adjustable via `PlayerStats.gd`
- No errors on startup or scene load
- Movement is responsive on keyboard

---

### 📝 Notes / Considerations
- Use a `CharacterBody2D` for future compatibility with slopes / collisions
- Keep logic modular in case we add dash mechanics later
