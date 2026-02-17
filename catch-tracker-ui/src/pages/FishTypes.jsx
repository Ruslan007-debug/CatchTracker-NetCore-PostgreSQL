import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getAll } from "../api/fishTypes.api";
import "../CSS/FishTypes.css";
import { toast } from 'react-toastify';

const FishTypes = ()=>
{
        const navigate = useNavigate();
        const [fishTypes, setFishTypes] = useState([]);
        const [searchQuery, setSearchQuery] = useState('');
        const [loading, setLoading] = useState(false);

        const loadFishTypes = async(query ='')=>
            {
                setLoading(true);
                try
                {
                    const allTypes = await getAll(query);
                    setFishTypes(allTypes);
                }
                catch(error)
                {
                    console.error('Помилка завантаження:', error);
                    toast.error('Помилка завантаження даних');
                }
                finally
                {
                    setLoading(false);
                }
            };

        useEffect(() => {
            const timer = setTimeout(() => {
                loadFishTypes(searchQuery);
        }, 500);

            return () => clearTimeout(timer);
        }, [searchQuery]);

        const showFishInfo = (fish) => {
                
            toast(fish.description , {
            position: "top-right",
            autoClose: 15000,  
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
        });
    };


        return(
            <div className="fish-types-page">
            {/* Шапка з пошуком */}
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
                <button 
                    className="home-button"
                    onClick={() => navigate('/MainPage')}
                >
                    На головну
                </button>
            </div>

            {/* Заголовок */}
            <h1 className="page-title">Типи риб</h1>

            {/* Показуємо що шукаємо */}
            {searchQuery && (
                <p className="search-info">
                    Результати пошуку для: "<strong>{searchQuery}</strong>"
                </p>
            )}

            {/* Індикатор завантаження */}
            {loading && (
                <div className="loading">
                    <p>🔍 Завантаження...</p>
                </div>
            )}

            {/* Список типів риб */}
            {!loading && (
                <div className="fish-types-list">
                    {fishTypes.length === 0 ? (
                        <div className="no-results">
                            <p>😕 Нічого не знайдено</p>
                            {searchQuery && (
                                <p>Спробуйте інший запит або очистіть пошук</p>
                            )}
                        </div>
                    ) : (
                        fishTypes.map((type) => (
                            <div key={type.id} className="fish-type-card">
                                {/* Іконка зліва */}
                                <div className="fish-icon" style={{ backgroundImage: `url(${type.imageUrl})` }}></div>

                                {/* Інформація через дефіс */}
                                <div className="fish-info">
                                    <span className="fish-name">{type.typeName}</span>
                                    
                                    {type.favBait && (
                                        <>
                                            <span className="separator">  </span>
                                            <span className="fish-description">
                                               🪱  Улюблені наживки - {type.favBait}
                                            </span>
                                        </>
                                    )}
                                    {type.avgWeight && (
                                        <>
                                            <span className="separator">  </span>
                                            <span className="fish-description">
                                                ⚖️ Середня вага - {type.avgWeight} кг
                                            </span>
                                        </>
                                    )}
                                </div>

                                {/* Кнопка детальніше (опціонально) */}
                                <button 
                                    className="details-btn"
                                    onClick={() => showFishInfo(type)}
                                    title="Детальніше"
                                >
                                    ℹ️
                                </button>
                            </div>
                        ))
                    )}
                </div>
            )}

            {/* Показуємо кількість результатів */}
            {!loading && fishTypes.length > 0 && (
                <p className="results-count">
                    Знайдено: {fishTypes.length} {fishTypes.length === 1 ? 'тип' : 'типів'}
                </p>
            )}
        </div>
        );

}

    export default FishTypes;