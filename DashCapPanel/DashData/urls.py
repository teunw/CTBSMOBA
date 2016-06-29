from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^characters', views.characters, name='characters'),
    url(r'^register', views.register, name='register'),
    url(r'^login', views.login, name='login'),
    url(r'^data', views.old_data, name='change-character')
]