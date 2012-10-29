﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   This program can be provided to you by Xceed Software Inc. under a
   proprietary commercial license agreement for use in non-Open Source
   projects. The commercial version of Extended WPF Toolkit also includes
   priority technical support, commercial updates, and many additional 
   useful WPF controls if you license Xceed Business Suite for WPF.

   Visit http://xceed.com and follow @datagrid on Twitter.

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Globalization;

namespace Xceed.Wpf.DataGrid
{
  internal class SelfPropertyDescriptor : PropertyDescriptor
  {
    public SelfPropertyDescriptor( Type columnDataType )
      : base( ".", null )
    {
      m_columnDataType = columnDataType;
    }

    public override AttributeCollection Attributes
    {
      get
      {
        if( typeof( IList ).IsAssignableFrom( this.PropertyType ) )
        {
          Attribute[] array = new Attribute[ base.Attributes.Count + 1 ];
          base.Attributes.CopyTo( array, 0 );
          array[ array.Length - 1 ] = new ListBindableAttribute( false );
          return new AttributeCollection( array );
        }

        return base.Attributes;
      }
    }

    public override Type ComponentType
    {
      get
      {
        return m_columnDataType;
      }
    }

    public override bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return m_columnDataType;
      }
    }

    public override bool CanResetValue( object component )
    {
      return false;
    }

    public override object GetValue( object component )
    {
      return component;
    }

    public override void ResetValue( object component )
    {
      throw new InvalidOperationException( "An attempt was made to set a value on a read-only property." );
    }

    public override void SetValue( object component, object value )
    {
      throw new InvalidOperationException( "An attempt was made to set a value on a read-only property." );
    }

    public override bool ShouldSerializeValue( object component )
    {
      return false;
    }

    private Type m_columnDataType;
  }
}