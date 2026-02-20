import { useState, useEffect, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { getAll } from "../api/fishTypes.api";
import "../CSS/FishTypes.css";
import { toast } from 'react-toastify';

const FishTypes = () => {
    const navigate = useNavigate();
    const [fishTypes, setFishTypes] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [loading, setLoading] = useState(false);
    
    // Стан для фільтрації: 'all' (всі), 'predator' (хижаки), 'peaceful' (мирні)
    const [filterType, setFilterType] = useState('all');

    const loadFishTypes = async (query = '') => {
        setLoading(true);
        try {
            const allTypes = await getAll(query);
            setFishTypes(allTypes);
        } catch (error) {
            console.error('Помилка завантаження:', error);
            toast.error('Помилка завантаження даних');
        } finally {
            setLoading(false);
        }
    };

    // Завантаження даних при зміні пошукового запиту
    useEffect(() => {
        const timer = setTimeout(() => {
            loadFishTypes(searchQuery);
        }, 500);

        return () => clearTimeout(timer);
    }, [searchQuery]);

    // Логіка фільтрації списку риб на основі обраної кнопки
    const filteredFish = useMemo(() => {
        return fishTypes.filter(fish => {
            if (filterType === 'all') return true;
            if (filterType === 'predator') return fish.isPredatory === true;
            if (filterType === 'peaceful') return fish.isPredatory === false;
            return true;
        });
    }, [fishTypes, filterType]);

    const showFishInfo = (fish) => {
        toast(fish.description || "Опис відсутній", {
            position: "top-right",
            autoClose: 10000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            theme: "dark"
        });
    };

    return (
        <div className="fish-types-page">
            {/* Шапка з пошуком та фільтрами */}
            <div className="page-header">
                <div className="search-container">
                    <input
                        type="text"
                        className="search-input"
                        placeholder="Пошук по назві..."
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                    />
                    <span className="search-icon">🔍</span>
                </div>

                {/* Фільтр по типу риби (хижак/мирна) */}
                <div className="type-filter">
                    <button 
                        className={`filter-btn ${filterType === 'all' ? 'active' : ''}`}
                        onClick={() => setFilterType('all')}
                    >
                        Всі види
                    </button>
                    <button 
                        className={`filter-btn ${filterType === 'predator' ? 'active' : ''}`}
                        onClick={() => setFilterType('predator')}
                    >
                        🦈 Хижаки
                    </button>
                    <button 
                        className={`filter-btn ${filterType === 'peaceful' ? 'active' : ''}`}
                        onClick={() => setFilterType('peaceful')}
                    >
                        🌿 Мирні
                    </button>
                </div>

                <button 
                    className="home-button"
                    onClick={() => navigate('/MainPage')}
                >
                    На головну
                </button>
            </div>

            <h1 className="page-title">Енциклопедія риб</h1>

            {searchQuery && (
                <p className="search-info">
                    Результати пошуку для: "<strong>{searchQuery}</strong>"
                </p>
            )}

            {loading ? (
                <div className="loading">
                    <div className="spinner"></div>
                    <p>Завантажуємо види риб...</p>
                </div>
            ) : (
                <div className="fish-types-list">
                    {filteredFish.length === 0 ? (
                        <div className="no-results">
                            <p>😕 У цій категорії нічого не знайдено</p>
                            <small>Спробуйте змінити фільтр або запит пошуку</small>
                        </div>
                    ) : (
                        filteredFish.map((type) => (
                            <div key={type.id} className="fish-type-card">
                                {/* Фото риби */}
                                <div 
                                    className="fish-icon" 
                                    style={{ backgroundImage: `url(${type.imageUrl || '/images/placeholder-fish.png'})` }}
                                ></div>

                                {/* Основна інформація */}
                                <div className="fish-info">
                                    <div className="fish-name-row">
                                        <span className="fish-name">{type.typeName}</span>
                                        <span className={`nature-badge ${type.isPredatory ? 'predator' : 'peaceful'}`}>
                                            {type.isPredatory ? '🦈 Хижак' : '🌿 Мирна'}
                                        </span>
                                    </div>
                                    
                                    <div className="fish-details-row">
                                        {type.favBait && (
                                            <span className="fish-detail-item">
                                                <strong>🪱 Наживка:</strong> {type.favBait}
                                            </span>
                                        )}
                                        {type.avgWeight && (
                                            <span className="fish-detail-item">
                                                <span className="separator"> | </span>
                                                <strong>⚖️ Сер. вага:</strong> {type.avgWeight} кг
                                            </span>
                                        )}
                                    </div>
                                </div>

                                {/* Кнопка виклику опису через Toast */}
                                <button 
                                    className="details-btn"
                                    onClick={() => showFishInfo(type)}
                                    title="Показати повний опис"
                                >
                                    ℹ️
                                </button>
                            </div>
                        ))
                    )}
                </div>
            )}

            {/* Футер з лічильником */}
            {!loading && filteredFish.length > 0 && (
                <p className="results-count">
                    Показано: {filteredFish.length} {filteredFish.length === 1 ? 'вид' : 'видів'}
                </p>
            )}
        </div>
    );
};

export default FishTypes;