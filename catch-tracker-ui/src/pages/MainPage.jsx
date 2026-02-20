import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { getAll as getFishTypes } from "../api/fishTypes.api";
import { getAll as getPlaces } from "../api/places.api";
import { getMyLogs, createLog, updateLog, deleteLog } from "../api/fishingLogs.api";
import { toast } from "react-toastify";
import pond1Img from "/images/ponds/notPredatorPond.jpg";
import pond2Img from "/images/ponds/PredatorPond.jpg";
import "../CSS/MainPage.css";

const PONDS = [
  {
    id: 1,
    name: "Ставок №1",
    subtitle: "Мирна риба",
    emoji: "🌿",
    photo: pond1Img,
    accent: "#4ade80",
    accentDark: "#14532d",
    docks: [
      { x: 25, y: 15, placeId: 6 },
      { x: 62, y: 8, placeId: 1 },
      { x: 48, y: 18, placeId: 8 },
      { x: 81, y: 38, placeId: 2 },
      { x: 73, y: 82, placeId: 4 },
      { x: 24, y: 80, placeId: 7 },
    ],
  },
  {
    id: 2,
    name: "Ставок №2",
    subtitle: "Хижа риба",
    emoji: "⚡",
    photo: pond2Img,
    accent: "#f97316",
    accentDark: "#7c2d12",
    docks: [
      { x: 48, y: 12 },
      { x: 88, y: 35 },
      { x: 80, y: 70 },
      { x: 52, y: 88 },
      { x: 22, y: 82 },
      { x: 8, y: 52 },
    ],
  },
];

// --- IMAGE SELECT ---
const ImageSelect = ({ options, value, onChange, placeholder, getLabel, getImage, getId }) => {
  const [open, setOpen] = useState(false);
  const selected = options.find((o) => getId(o) === value);

  useEffect(() => {
    if (!open) return;
    const close = () => setOpen(false);
    document.addEventListener("click", close);
    return () => document.removeEventListener("click", close);
  }, [open]);

  return (
    <div className="img-select" onClick={(e) => { e.stopPropagation(); setOpen((v) => !v); }}>
      <div className="img-select__trigger">
        {selected ? (
          <>
            {getImage(selected) && <img src={getImage(selected)} alt="" className="img-select__thumb" />}
            <span>{getLabel(selected)}</span>
          </>
        ) : (
          <span className="img-select__placeholder">{placeholder}</span>
        )}
        <span className="img-select__arrow">{open ? "▲" : "▼"}</span>
      </div>
      {open && (
        <div className="img-select__dropdown" onClick={(e) => e.stopPropagation()}>
          {options.length === 0 && <div className="img-select__empty">Немає варіантів</div>}
          {options.map((opt) => (
            <div
              key={getId(opt)}
              className={`img-select__option ${getId(opt) === value ? "img-select__option--active" : ""}`}
              onClick={() => { onChange(getId(opt)); setOpen(false); }}
            >
              {getImage(opt) && <img src={getImage(opt)} alt="" className="img-select__thumb" />}
              <span className="img-select__option-name">{getLabel(opt)}</span>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

// --- CATCH FORM MODAL ---
const CatchForm = ({ pond, fishTypes, places, onClose, onSave, editingLog }) => {
  const [form, setForm] = useState({
    fishTypeId: editingLog?.fishType?.id ?? "",
    placeId: editingLog?.place?.id ?? "",
    trophy: editingLog?.trophy ?? "",
    weight: editingLog?.weight ?? "",
    bait: editingLog?.bait ?? "",
    distance: editingLog?.distance ?? "",
  });
  const [saving, setSaving] = useState(false);

  const filteredFish = fishTypes.filter((f) => pond.id === 1 ? !f.isPredatory : f.isPredatory);
  const filteredPlaces = places.filter((p) => p.pondId === pond.id);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!form.fishTypeId || !form.placeId) { toast.error("Оберіть рибу та місце"); return; }
    setSaving(true);
    try {
      await onSave({
        fishTypeId: Number(form.fishTypeId),
        placeId: Number(form.placeId),
        trophy: form.trophy,
        weight: Number(form.weight),
        bait: form.bait,
        distance: Number(form.distance),
      });
      onClose();
    } catch { toast.error("Помилка збереження"); }
    finally { setSaving(false); }
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal modal--large" onClick={(e) => e.stopPropagation()}>
        <div className="modal__header" style={{ background: `linear-gradient(135deg, ${pond.accentDark}, #0a0f0d)` }}>
          <span className="modal__emoji">{pond.emoji}</span>
          <h2>{editingLog ? "Редагувати улов" : "Реєстрація улову"}</h2>
          <p className="modal__sub">{pond.name} · {pond.subtitle}</p>
          <button className="modal__close" onClick={onClose}>✕</button>
        </div>

        <form className="modal__body" onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label>🐟 Вид риби</label>
              <ImageSelect
                options={filteredFish}
                value={form.fishTypeId}
                onChange={(v) => setForm({ ...form, fishTypeId: v })}
                placeholder="Оберіть рибу..."
                getLabel={(f) => f.typeName}
                getImage={(f) => f.imageUrl}
                getId={(f) => f.id}
              />
            </div>
            <div className="form-group">
              <label>📍 Місце лову</label>
              <ImageSelect
                options={filteredPlaces}
                value={form.placeId}
                onChange={(v) => setForm({ ...form, placeId: v })}
                placeholder="Оберіть місце..."
                getLabel={(p) => p.name}
                getImage={(p) => p.imgUrl}
                getId={(p) => p.id}
              />
            </div>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label>🏆 Трофей</label>
              <input type="text" placeholder="Назва трофею..." value={form.trophy}
                onChange={(e) => setForm({ ...form, trophy: e.target.value })} />
            </div>
            <div className="form-group">
              <label>⚖️ Вага (кг)</label>
              <input type="number" step="0.01" placeholder="0.00" value={form.weight} required
                onChange={(e) => setForm({ ...form, weight: e.target.value })} />
            </div>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label>🪱 Наживка</label>
              <input type="text" placeholder="Що використовували..." value={form.bait}
                onChange={(e) => setForm({ ...form, bait: e.target.value })} />
            </div>
            <div className="form-group">
              <label>🎣 Дистанція (м)</label>
              <input type="number" step="0.1" placeholder="0.0" value={form.distance}
                onChange={(e) => setForm({ ...form, distance: e.target.value })} />
            </div>
          </div>

          <button type="submit" className="modal__submit" style={{ background: pond.accent }} disabled={saving}>
            {saving ? "Збереження..." : editingLog ? "Зберегти зміни ✓" : "Зареєструвати улов 🎣"}
          </button>
        </form>
      </div>
    </div>
  );
};

// --- LOG CARD ---
const LogCard = ({ log, onEdit, onDelete, accent }) => {
  const [confirmDelete, setConfirmDelete] = useState(false);
  return (
    <div className="log-card">
      <div className="log-card__banner"
        style={{ backgroundImage: log.place?.imgUrl ? `url(${log.place.imgUrl})` : "none" }}>
        <div className="log-card__avatar">
          {log.fishType?.imageUrl
            ? <img src={log.fishType.imageUrl} alt={log.fishType.typeName} />
            : <span>🐟</span>}
        </div>
        <div className="log-card__weight" style={{ background: accent }}>{log.weight} кг</div>
      </div>
      <div className="log-card__body">
        <h3>{log.fishType?.typeName || "—"}</h3>
        <p className="log-card__place">📍 {log.place?.name || "—"}</p>
        <div className="log-card__tags">
          {log.bait && <span>🪱 {log.bait}</span>}
          {log.distance > 0 && <span>🎣 {log.distance} м</span>}
          {log.trophy && <span>🏆 {log.trophy}</span>}
        </div>
        <div className="log-card__date">{new Date(log.time).toLocaleDateString("uk-UA")}</div>
        <div className="log-card__actions">
          <button className="lcbtn lcbtn--edit" onClick={() => onEdit(log)}>✏️ Редагувати</button>
          {confirmDelete ? (
            <div className="log-card__confirm">
              <span>Видалити?</span>
              <button className="lcbtn lcbtn--yes" onClick={() => onDelete(log.id)}>Так</button>
              <button className="lcbtn" onClick={() => setConfirmDelete(false)}>Ні</button>
            </div>
          ) : (
            <button className="lcbtn lcbtn--del" onClick={() => setConfirmDelete(true)}>🗑️ Видалити</button>
          )}
        </div>
      </div>
    </div>
  );
};

// --- DOCK MARKER ---
const DockMarker = ({ dock, logs, places, pond, onAddClick }) => {
  const [showPopup, setShowPopup] = useState(false);

  const place = places.find((p) => p.id === dock.placeId);
  const dockLogs = logs.filter((l) => (l.place?.id ?? l.placeId) === dock.placeId);
  const count = dockLogs.length;

  // Динамічний клас для відкриття попапу вниз, якщо точка занадто високо
  const popupClass = dock.y < 30 ? "dock-popup dock-popup--down" : "dock-popup";

  return (
    <div
      className="dock-wrap"
      style={{ left: `${dock.x}%`, top: `${dock.y}%` }}
      onMouseEnter={() => setShowPopup(true)}
      onMouseLeave={() => setShowPopup(false)}
    >
      {showPopup && (
        <div className={popupClass}>
          <div className="dock-popup__header" style={{ borderColor: pond.accent }}>
            <span className="dock-popup__name">{place?.name || "Кладка"}</span>
            <span className="dock-popup__count" style={{ color: pond.accent }}>🎣 {count}</span>
          </div>
          {dockLogs.length === 0 ? (
            <p className="dock-popup__empty"></p>
          ) : (
            <ul className="dock-popup__list">
              {dockLogs.map((log) => (
                <li key={log.id} className="dock-popup__item">
                  {log.fishType?.imageUrl && (
                    <img src={log.fishType.imageUrl} alt="" className="dock-popup__fish-img" />
                  )}
                  <div className="dock-popup__fish-info">
                    <span className="dock-popup__fish-name">{log.fishType?.typeName || "—"}</span>
                    <span className="dock-popup__fish-weight" style={{ color: pond.accent }}>{log.weight} кг</span>
                  </div>
                  <span className="dock-popup__fish-date">
                    {new Date(log.time).toLocaleDateString("uk-UA", { day: 'numeric', month: 'short' })}
                  </span>
                </li>
              ))}
            </ul>
          )}
          <button
            className="dock-popup__add"
            style={{ background: pond.accent }}
            onClick={(e) => { e.stopPropagation(); onAddClick(); }}
          >
            + Додати улов
          </button>
        </div>
      )}

      <button
        className={`dock-btn ${count > 0 ? "dock-btn--has-catches" : ""}`}
        style={{ "--accent": pond.accent }}
        onClick={onAddClick}
      >
        {count > 0 ? (
          <span className="dock-btn__count">{count}</span>
        ) : (
          <span className="dock-btn__icon">+</span>
        )}
        {count === 0 && <span className="dock-btn__pulse" />}
      </button>
    </div>
  );
};

const PondMap = ({ pond, logs, places, onDockClick }) => {
  return (
    <div className="pond-map">
      <img src={pond.photo} alt={pond.name} className="pond-map__img" draggable={false} />
      {pond.docks.map((dock, i) => (
        <DockMarker
          key={i}
          dock={dock}
          logs={logs}
          places={places}
          pond={pond}
          onAddClick={onDockClick}
        />
      ))}
    </div>
  );
};

const MainPage = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const [activePond, setActivePond] = useState(1);
  const [fishTypes, setFishTypes] = useState([]);
  const [places, setPlaces] = useState([]);
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingLog, setEditingLog] = useState(null);

  const pond = PONDS.find((p) => p.id === activePond);
  const pondLogs = logs.filter((log) => {
    const place = places.find((p) => p.id === (log.place?.id ?? log.placeId));
    return place?.pondId === activePond;
  });

  useEffect(() => {
    (async () => {
      setLoading(true);
      try {
        const [ft, pl, lg] = await Promise.all([getFishTypes(), getPlaces(), getMyLogs()]);
        setFishTypes(ft);
        setPlaces(pl);
        setLogs(lg);
      } catch { toast.error("Помилка завантаження даних"); }
      finally { setLoading(false); }
    })();
  }, []);

  const handleSave = async (formData) => {
    if (editingLog) {
      const updated = await updateLog(editingLog.id, formData);
      setLogs((prev) => prev.map((l) => (l.id === editingLog.id ? updated : l)));
      toast.success("Улов оновлено! ✓");
    } else {
      const created = await createLog(formData);
      setLogs((prev) => [...prev, created]);
      toast.success("Улов зареєстровано! 🎣");
    }
    setEditingLog(null);
  };

  const openAdd = () => { setEditingLog(null); setShowForm(true); };
  const openEdit = (log) => { setEditingLog(log); setShowForm(true); };
  const handleDelete = async (id) => {
    try {
      await deleteLog(id);
      setLogs((prev) => prev.filter((l) => l.id !== id));
      toast.success("Улов видалено");
    } catch { toast.error("Помилка видалення"); }
  };

  return (
    <div className="main-page">
      <header className="main-header">
        <div className="main-header__brand">
          <span className="main-header__logo">🎣 Catch Tracker</span>
          <span className="main-header__tagline">Твій особистий щоденник риболовлі</span>
        </div>
        <nav className="main-header__nav">
          <button onClick={() => navigate("/fishTypes")}>Риби</button>
          <button onClick={() => navigate("/places")}>Місця</button>
          <button onClick={() => navigate("/userLogs")}>Мої улови</button>
          <button onClick={() => navigate("/leaderboard")}>🏆 Лідери</button>
        </nav>
        <div className="main-header__user">
          <span>Вітаємо, <strong>{user?.username || user?.name}</strong>!</span>
          <button className="main-header__logout" onClick={() => { logout(); navigate("/login"); }}>Вийти</button>
        </div>
      </header>

      <div className="pond-tabs">
        {PONDS.map((p) => (
          <button
            key={p.id}
            className={`pond-tab ${activePond === p.id ? "pond-tab--active" : ""}`}
            style={activePond === p.id ? { borderColor: p.accent, color: p.accent } : {}}
            onClick={() => setActivePond(p.id)}
          >
            <span className="pond-tab__emoji">{p.emoji}</span>
            <div>
              <div className="pond-tab__name">{p.name}</div>
              <div className="pond-tab__sub">{p.subtitle}</div>
            </div>
          </button>
        ))}
      </div>

      <div className="pond-layout">
        <div className="pond-layout__map">
          <div className="pond-layout__map-header">
            <div>
              <h2 style={{ color: pond.accent }}>{pond.emoji} {pond.name}</h2>
              <p className="pond-layout__hint">Натисни <strong>+</strong> біля кладки щоб додати улов</p>
            </div>
            <button className="add-btn" style={{ background: pond.accent }} onClick={openAdd}>
              + Додати улов
            </button>
          </div>
          {loading ? (
            <div className="pond-loading">⏳ Завантаження...</div>
          ) : (
            <PondMap pond={pond} logs={logs} places={places} onDockClick={openAdd} />
          )}
        </div>

        <div className="pond-layout__logs">
          <div className="pond-layout__logs-header">
            <h3>Улови на {pond.name}</h3>
            <span className="log-count" style={{ background: pond.accent }}>{pondLogs.length}</span>
          </div>
          {!loading && pondLogs.length === 0 && (
            <div className="pond-empty">
              <div className="pond-empty__icon">🎣</div>
              <p>Ще немає уловів</p>
            </div>
          )}
          <div className="logs-grid">
            {pondLogs.map((log) => (
              <LogCard key={log.id} log={log} accent={pond.accent} onEdit={openEdit} onDelete={handleDelete} />
            ))}
          </div>
        </div>
      </div>

      {showForm && (
        <CatchForm
          pond={pond}
          fishTypes={fishTypes}
          places={places}
          onClose={() => { setShowForm(false); setEditingLog(null); }}
          onSave={handleSave}
          editingLog={editingLog}
        />
      )}
    </div>
  );
};

export default MainPage;