using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;


public class BannerADS : MonoBehaviour
{
       
    [SerializeField] private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
    BannerView _bannerView;


    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
     
        // Inicializa o SDK do Google Mobile Ads
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            CreateBannerView();
            LoadAd();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Criando banner view");

        // Se já houver um banner, destrói o antigo
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Cria um banner 320x50 no topo da tela
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
        ListenToAdEvents();
    }

    [Obsolete]
    public void LoadAd()
    {
        // Cria uma instância de uma banner view primeiro
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        // Cria nossa solicitação usada para carregar o anúncio
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // Envia a solicitação para carregar o anúncio
        Debug.Log("Carregando anúncio de banner.");
        _bannerView.LoadAd(adRequest);
    }

    private void ListenToAdEvents()
    {
        // Chamado quando um anúncio é carregado na banner view
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view carregou um anúncio com resposta: "
                + _bannerView.GetResponseInfo());
        };
        // Chamado quando um anúncio falha ao carregar na banner view
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view falhou ao carregar um anúncio com erro: "
                + error);
        };
        // Chamado quando é estimado que o anúncio ganhou dinheiro
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view pagou {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Chamado quando uma impressão é registrada para um anúncio
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view registrou uma impressão.");
        };
        // Chamado quando um clique é registrado para um anúncio
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view foi clicado.");
        };
        // Chamado quando um anúncio abre conteúdo em tela cheia
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view abriu conteúdo em tela cheia.");
        };
        // Chamado quando o conteúdo em tela cheia de um anúncio é fechado
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view fechou o conteúdo em tela cheia.");
        };
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destruindo anúncio de banner.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
